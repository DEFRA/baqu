using BAQU.Entities;
using BAQU.Repository;

namespace BAQU.Services;

public class DAXService : IDAXService
{
    private readonly IDAXRepository _daxRepository;
    private readonly ILogger<DAXService> _logger;

    public DAXService(IDAXRepository daxRepository, ILogger<DAXService> logger)
    {
        _daxRepository = daxRepository;
        _logger = logger;
    }

    public virtual async Task<List<RSFDwhVendVendorBankAccountStaging>> GetQualityCheckData(DateTime lastChange)
    {
        _logger.LogInformation("Getting quality check data.");
        try
        {
            var (updatedAccounts, expiredAccounts) = await _daxRepository.Get(lastChange);

            var updatedFrns = new HashSet<string>(updatedAccounts.Select(y => y.VENDACCOUNT));
            updatedAccounts.AddRange(expiredAccounts.Where(x => !updatedFrns.Contains(x.VENDACCOUNT)).ToList());

            return updatedAccounts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting quality check data.");
            throw new ApplicationException("An error occurred while getting quality check data.", ex);
        }
    }
}
