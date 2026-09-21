using Microsoft.EntityFrameworkCore;
using BAQU.Data;
using BAQU.Entities;

namespace BAQU.Repository;

public class DAXRepository : IDAXRepository
{
    private readonly IConfiguration _configuration;
    private readonly IDbContextFactory<DAXContext> _contextFactory;

    private readonly DateTime defaultDate = new DateTime(1900, 01, 01);
    private readonly List<string> _excludeList;

    public DAXRepository(IConfiguration configuration, IDbContextFactory<DAXContext> contextFactory)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        
        _excludeList = _configuration.GetSection("Options:ExcludeList").Get<List<string>>() ?? [];
    }

    public virtual async Task<(List<RSFDwhVendVendorBankAccountStaging> updatedAccounts, List<RSFDwhVendVendorBankAccountStaging> expiredAccounts)> Get(DateTime lastChange)
    {
        var updatedAccountsTask = GetUpdatedBankAccounts(lastChange);
        var expiredAccountsTask = GetExpiredBankAccounts(lastChange);
        await Task.WhenAll(updatedAccountsTask, expiredAccountsTask);
        
        return (await updatedAccountsTask, await expiredAccountsTask);
    }

    private async Task<List<RSFDwhVendVendorBankAccountStaging>> GetUpdatedBankAccounts(DateTime lastChange)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.DAXVendBankAccount.Where(x => x.MODIFIEDDATETIME > lastChange 
            && x.MODIFIEDDATETIME < DateTime.Today 
            && !_excludeList.Contains(x.MODIFIEDBY) 
            && x.EXPIRYDATE == defaultDate).ToListAsync();
    }

    private async Task<List<RSFDwhVendVendorBankAccountStaging>> GetExpiredBankAccounts(DateTime lastChange)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.DAXVendBankAccount.Where(x => x.MODIFIEDDATETIME > lastChange 
            && x.MODIFIEDDATETIME < DateTime.Today 
            && !_excludeList.Contains(x.MODIFIEDBY) 
            && x.EXPIRYDATE != defaultDate).ToListAsync();
    }
}
