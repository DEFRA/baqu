using BAQU.Entities;
using BAQU.Repository;

namespace BAQU.Services;

public interface IDAXService
{
    Task<List<RSFDwhVendVendorBankAccountStaging>> GetQualityCheckData(DateTime lastChange);
}
