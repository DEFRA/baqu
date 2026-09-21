using System.Diagnostics.CodeAnalysis;
using BAQU.Data;
using BAQU.Providers;
using BAQU.Repository;
using BAQU.Services;

namespace BAQU.Collections;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceLogService, ServiceLogService>();
        services.AddScoped<IServiceLogRepository, ServiceLogRepository>();

        services.AddScoped<IQualityCheckService, QualityCheckService>();
        services.AddScoped<IQualityCheckDBRepository, QualityCheckDBRepository>();

        services.AddScoped<IPeopleService, PeopleService>();
        services.AddScoped<IPeopleRepository, PeopleRepository>();

        services.AddScoped<IDAXRepository, DAXRepository>();
        services.AddScoped<IDAXService, DAXService>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
