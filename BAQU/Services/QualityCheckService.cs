using BAQU.Entities;
using BAQU.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace BAQU.Services;

public class QualityCheckService : IQualityCheckService
{
    private readonly IQualityCheckDBRepository _qualityCheckDBRepository;
    private readonly IPeopleService _peopleService;
    private readonly IServiceLogService _serviceLogService;
    private readonly ILogger<QualityCheckService> _logger;
    
    public QualityCheckService(IQualityCheckDBRepository qualityCheckDBRepository, IPeopleService peopleService, IServiceLogService serviceLogService, ILogger<QualityCheckService> logger)
    {
        _qualityCheckDBRepository = qualityCheckDBRepository ?? throw new ArgumentNullException(nameof(qualityCheckDBRepository));
        _peopleService = peopleService ?? throw new ArgumentNullException(nameof(peopleService));
        _serviceLogService = serviceLogService ?? throw new ArgumentNullException(nameof(serviceLogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public virtual async Task CreateQualityChecks(List<RSFDwhVendVendorBankAccountStaging> updatedAccounts, DateTime lastChange)
    {
        if (updatedAccounts == null)
        {
            throw new ArgumentNullException(nameof(updatedAccounts));
        }

        if (lastChange == default)
        {
            _logger.LogWarning("Last change date is set to default value. This may result in processing too many records.");
        }

        _logger.LogInformation("Generating quality checks started...");
        try
        {
            DateTime? maxModifiedDate = lastChange;
            List<QualityCheckDetails> qualityChecksToAdd = new List<QualityCheckDetails>();

            // Prepare, dont save
            foreach (var account in updatedAccounts)
            {
                if (account == null)
                {
                    _logger.LogWarning("Null account encountered in updatedAccounts. Skipping.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(account.VENDACCOUNT))
                {
                    _logger.LogWarning("Account with empty VENDACCOUNT encountered. Skipping.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(account.MODIFIEDBY))
                {
                    _logger.LogWarning($"Account with VENDACCOUNT {account.VENDACCOUNT} has no ModifiedBy value. Using default.");
                }

                var person = await _peopleService.GetPerson(account.MODIFIEDBY ?? "Unknown");

                if (!int.TryParse(account.VENDACCOUNT, out int VENDACCOUNTValue))
                {
                    _logger.LogWarning($"Unable to parse VENDACCOUNT {account.VENDACCOUNT} as integer. Skipping.");
                    continue;
                }

                var qc = new QualityCheckDetails
                {
                    PersonName = $"{person.Surname}, {person.GivenName}" ?? "Unknown",
                    FRN = VENDACCOUNTValue,
                    BusinessName = account.BUSINESSNAME ?? "Unknown",
                    ManagerName = person.Manager != null && person.Manager.AdditionalData != null && person.Manager.AdditionalData.ContainsKey("displayName") 
                        ? $"{person.Manager.AdditionalData["surname"].ToString()}, {person.Manager.AdditionalData["givenName"].ToString()}" ?? "Unknown" 
                        : "Unknown",
                    DateQCCreated = account.MODIFIEDDATETIME,
                };

                _logger.LogInformation("Preparing quality check for VENDACCOUNT: {VENDACCOUNT}", qc.FRN);
                qualityChecksToAdd.Add(qc);

                if (!maxModifiedDate.HasValue || account.MODIFIEDDATETIME > maxModifiedDate.Value)
                {
                    maxModifiedDate = account.MODIFIEDDATETIME;
                }
            }

            if (qualityChecksToAdd.Count == 0)
            {
                _logger.LogInformation("No valid quality checks to create.");
                return;
            }

            // Perform all database operations within a transaction
            _logger.LogInformation("Starting transaction to save {count} quality checks...", qualityChecksToAdd.Count);
            
            await _qualityCheckDBRepository.ExecuteInTransactionAsync(async () =>
            {
                _qualityCheckDBRepository.AddQualityChecks(qualityChecksToAdd);
                
                _logger.LogInformation("Saving quality checks...");
                await _qualityCheckDBRepository.SaveQualityCheck();

                _logger.LogInformation("Updating service log...");
                await _serviceLogService.UpdateServiceLog(maxModifiedDate.Value, qualityChecksToAdd.Count);
                
                _logger.LogInformation("Transaction committed successfully.");
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating quality checks.");
            throw new ApplicationException("An error occurred while creating quality checks.", ex);
        }
    }
}
