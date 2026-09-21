using System.Diagnostics;
using BAQU.Collections;
using BAQU.Workers;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Options;

var logSourceName = "BAQU";
var logName = "BAQU";

if(!EventLog.SourceExists(logSourceName))
{
    EventLog.CreateEventSource(logSourceName, logName);
}

var host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "BAQUService";
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        
        if (hostingContext.HostingEnvironment.IsDevelopment())
        {
            config.AddUserSecrets<Program>();
        }
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Configure WorkerOptions from command line arguments
        services.Configure<WorkerOptions>(options => 
        {
            options.ForceExecution = args.Contains("--force") || args.Contains("-f");
        });
        
        services.AddServices();
        services.AddDatabases(hostContext.Configuration);
        services.AddHostedService<Worker>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddEventLog(l => 
        {
            l.SourceName = logSourceName;
            l.LogName = logName;
        });

    })
    .Build();

host.Run();
