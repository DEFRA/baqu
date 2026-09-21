using Microsoft.EntityFrameworkCore;
using BAQU.Data;
using BAQU.Entities;

namespace BAQU.Repository;

public interface IServiceLogRepository
{
    Task<List<ServiceLogs>> Get();
    Task SaveServiceLogEntry(ServiceLogs serviceLog);
}
