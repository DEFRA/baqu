using BAQU.Entities;
using BAQU.Repository;

namespace BAQU.Services;

public class ServiceLogService : IServiceLogService
{
    private readonly IServiceLogRepository _serviceLogRepository;
    private readonly ILogger<ServiceLogService> _logger;

    public ServiceLogService(IServiceLogRepository serviceLogRepository, ILogger<ServiceLogService> logger)
    {
        _serviceLogRepository = serviceLogRepository;
        _logger = logger;
    }

    public virtual async Task<DateTime> GetLastChange()
    {
        _logger.LogInformation("Getting the last change date from service log.");
        try
        {
            var logs = await _serviceLogRepository.Get();
            return logs.Count > 0 ? logs.Max(static x => x.LastChange) : DateTime.Today.AddDays(-3);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the last change date from service log.");
            throw new ApplicationException("An error occurred while getting the last change date from service log.", ex);
        }
    }
    
    public virtual async Task UpdateServiceLog(DateTime lastChange, int ChecksMadeCount)
    {
        _logger.LogInformation($"Updating service log. Last Change: {lastChange} QC Generated: {ChecksMadeCount}");
        try
        {
            var logEntry = new ServiceLogs 
            {
                LastChange = lastChange,
                Changes = ChecksMadeCount
            };

            await _serviceLogRepository.SaveServiceLogEntry(logEntry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the service log.");
            throw new ApplicationException("An error occurred while updating the service log.", ex);
        }
    }
}
