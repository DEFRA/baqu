using Microsoft.EntityFrameworkCore;
using BAQU.Data;
using BAQU.Entities;

namespace BAQU.Repository;

public class ServiceLogRepository : IServiceLogRepository
{
    private readonly ServiceLogContext _serviceLogContext;

    public ServiceLogRepository(ServiceLogContext serviceLogContext)
    {
        _serviceLogContext = serviceLogContext;
    }

    public virtual async Task<List<ServiceLogs>> Get()
        => await _serviceLogContext.ServiceLog.ToListAsync();

    public virtual async Task SaveServiceLogEntry(ServiceLogs serviceLog) 
    {
        _serviceLogContext.ServiceLog.Add(serviceLog);
        await _serviceLogContext.SaveChangesAsync();
    }

}
