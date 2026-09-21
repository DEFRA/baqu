using BAQU.Providers;
using BAQU.Services;
using Microsoft.Extensions.Options;
using NCrontab;

namespace BAQU.Workers;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<Worker> _logger;
    private IConfiguration _configuration;
    private CrontabSchedule _schedule;
    private DateTime _nextRun;
    private readonly bool _force;

    public Worker(
        IServiceProvider serviceProvider,
        IDateTimeProvider dateTimeProvider,
        IConfiguration configuration,
        ILogger<Worker> logger,
        IOptions<WorkerOptions> options)
    {
        _serviceProvider = serviceProvider;
        _dateTimeProvider = dateTimeProvider;
        _configuration = configuration;
        _logger = logger;
        _schedule = CrontabSchedule.Parse(_configuration["Options:ExecutionSchedule"]);
        _nextRun = _schedule.GetNextOccurrence(_dateTimeProvider.Now);
        _force = options.Value.ForceExecution;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                var now = _dateTimeProvider.Now;
                if (_force || now > _nextRun)
                {
                    _logger.LogInformation("Worker running at: {time}", new DateTimeOffset(_dateTimeProvider.Now));

                    try
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var daxService = scope.ServiceProvider.GetRequiredService<IDAXService>();
                            var serviceLogService = scope.ServiceProvider.GetRequiredService<IServiceLogService>();
                            var qualityCheckService = scope.ServiceProvider.GetRequiredService<IQualityCheckService>();
                        
                            _logger.LogInformation("Getting last change date...");

                            // This section is only needed becuase the DBs keep sleeping and the first connection has a good chance of failing.
                            DateTime lastChange;
                            int retryCount = 0;
                            const int maxRetries = 3;
                            
                            while (true)
                            {
                                try
                                {
                                    lastChange = await serviceLogService.GetLastChange();
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    retryCount++;
                                    if (retryCount >= maxRetries)
                                    {
                                        _logger.LogError(ex, "Failed to get last change date after {retryCount} attempts", retryCount);
                                        throw; 
                                    }
                                    
                                    _logger.LogWarning(ex, "Failed to get last change date (attempt {current}/{total}), waiting 10 seconds before retrying...", 
                                        retryCount, maxRetries);
                                    await Task.Delay(10000, stoppingToken);
                                }
                            }
                            
                            _logger.LogInformation("Retrieving quality check data since {lastChange}...", lastChange);
                            var updatedAccounts = await daxService.GetQualityCheckData(lastChange);
                            
                            if (updatedAccounts.Count > 0)
                            {
                                _logger.LogInformation("Creating {count} quality checks...", updatedAccounts.Count);
                                await qualityCheckService.CreateQualityChecks(updatedAccounts, lastChange);
                            }
                            else
                            {
                                _logger.LogInformation("No accounts to process.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred during worker execution.");
                    }
                    _logger.LogInformation("Worker finished at: {time}", new DateTimeOffset(_dateTimeProvider.Now));

                    _nextRun = _schedule.GetNextOccurrence(_dateTimeProvider.Now);
                }
            }
            await Task.Delay(60000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopped at: {time}", new DateTimeOffset(_dateTimeProvider.Now));
        await base.StopAsync(cancellationToken);
    }
}
