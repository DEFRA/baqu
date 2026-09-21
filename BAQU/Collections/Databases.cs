using System.Diagnostics.CodeAnalysis;
using BAQU.Data;
using Microsoft.EntityFrameworkCore;

namespace BAQU.Collections;

[ExcludeFromCodeCoverage]
public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DAXContext as a factory first
        services.AddDbContextFactory<DAXContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DAX"), o =>
            {
                o.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
            }));

        services.AddDbContext<QualityCheckDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("QC"), o =>
            {
                o.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                );
                o.CommandTimeout(60);
            }));

        services.AddDbContext<ServiceLogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("QC"), o =>
            {
                o.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
                o.CommandTimeout(60);
            }));
            
        services.AddDbContext<PeopleContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("People"), o =>
            {
                o.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
            }));
            
        return services;
    }
}
