using BAQU.Data;
using BAQU.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BAQU.Repository;

public class QualityCheckDBRepository : IQualityCheckDBRepository
{
    private readonly QualityCheckDBContext _qualityCheckDBContext;

    public QualityCheckDBRepository(QualityCheckDBContext qualityCheckDBContext)
    {
        _qualityCheckDBContext = qualityCheckDBContext;
    }

    public virtual void AddQualityCheck(QualityCheckDetails qualityCheck) 
        => _qualityCheckDBContext.QualityCheckDetails.Add(qualityCheck);

    public virtual void AddQualityChecks(IEnumerable<QualityCheckDetails> qualityChecks)
        => _qualityCheckDBContext.QualityCheckDetails.AddRange(qualityChecks);
    
    public virtual async Task SaveQualityCheck()
        => await _qualityCheckDBContext.SaveChangesAsync();
        
    public virtual IExecutionStrategy GetExecutionStrategy()
        => _qualityCheckDBContext.Database.CreateExecutionStrategy();
        
    public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
        => await _qualityCheckDBContext.Database.BeginTransactionAsync();
        
    public virtual async Task ExecuteInTransactionAsync(Func<Task> operation)
    {
        var strategy = GetExecutionStrategy();
        
        await strategy.ExecuteAsync<object, bool>(
            state: null,
            operation: async (context, state, cancellationToken) =>
            {
                using var transaction = await BeginTransactionAsync();
                try
                {
                    await operation();
                    await transaction.CommitAsync(cancellationToken);
                    return true;
                }
                catch
                {
                    throw;
                }
            },
            verifySucceeded: null);
    }
}
