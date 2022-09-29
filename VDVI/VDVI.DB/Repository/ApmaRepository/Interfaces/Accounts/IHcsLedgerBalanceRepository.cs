using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos; 

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsLedgerBalanceRepository
    {
        Task<LedgerBalanceDto> InsertAsync(LedgerBalanceDto dto);
        Task<IEnumerable<LedgerBalanceDto>> BulkInsertAsync(IEnumerable<LedgerBalanceDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<LedgerBalanceDto> dto);
        Task<LedgerBalanceDto> UpdateAsync(LedgerBalanceDto dto);
        Task<IEnumerable<LedgerBalanceDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<LedgerBalanceDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
