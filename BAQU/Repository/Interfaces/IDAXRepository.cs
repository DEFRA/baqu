using Microsoft.EntityFrameworkCore;
using BAQU.Data;
using BAQU.Entities;

namespace BAQU.Repository;

public interface IDAXRepository
{
    Task<(List<RSFDwhVendVendorBankAccountStaging> updatedAccounts, List<RSFDwhVendVendorBankAccountStaging> expiredAccounts)> Get(DateTime lastChange);
}
