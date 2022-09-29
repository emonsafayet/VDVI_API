using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos; 

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsLedgerBalanceHistoryRepository
    {
        Task<LedgerBalanceHistoryDto> InsertAsync(LedgerBalanceHistoryDto dto);
        Task<IEnumerable<LedgerBalanceHistoryDto>> BulkInsertAsync(IEnumerable<LedgerBalanceHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<LedgerBalanceHistoryDto> dto);
        Task<LedgerBalanceHistoryDto> UpdateAsync(LedgerBalanceHistoryDto dto);
        Task<IEnumerable<LedgerBalanceHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<LedgerBalanceHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
