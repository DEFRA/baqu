using BAQU.Entities;
using BAQU.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace BAQU.Services;

public interface IQualityCheckService
{
    Task CreateQualityChecks(List<RSFDwhVendVendorBankAccountStaging> updatedAccounts, DateTime lastChange);
}
