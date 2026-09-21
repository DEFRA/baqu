using BAQU.Entities;
using BAQU.Repository;

namespace BAQU.Services;

public interface IServiceLogService
{
    Task<DateTime> GetLastChange();
    Task UpdateServiceLog(DateTime lastChange, int ChecksMadeCount);
}
