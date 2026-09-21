using BAQU.Data;
using BAQU.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BAQU.Repository;

public interface IQualityCheckDBRepository
{
    void AddQualityCheck(QualityCheckDetails qualityCheck);
    void AddQualityChecks(IEnumerable<QualityCheckDetails> qualityChecks);
    Task SaveQualityCheck();
    IExecutionStrategy GetExecutionStrategy();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task ExecuteInTransactionAsync(Func<Task> operation);
}
