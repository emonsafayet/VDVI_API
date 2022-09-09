using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Models.Accounts;

namespace VDVI.Repository.Repository.Interfaces
{
    public interface ILedgerBalanceRepo
    {
        Task<DbLedgerBalance> InsertAsync(DbLedgerBalance dto);
        Task<IEnumerable<DbLedgerBalance>> BulkInsertAsync(IEnumerable<DbLedgerBalance> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<DbLedgerBalance> dto);
        Task<DbLedgerBalance> UpdateAsync(DbLedgerBalance dto);
        Task<IEnumerable<DbLedgerBalance>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<DbLedgerBalance> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
    }
}
