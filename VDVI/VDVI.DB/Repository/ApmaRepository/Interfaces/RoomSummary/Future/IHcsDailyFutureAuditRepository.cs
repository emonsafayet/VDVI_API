using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsDailyFutureAuditRepository
    {
        Task<DailyFutureAuditDto> InsertAsync(DailyFutureAuditDto dto);
        Task<IEnumerable<DailyFutureAuditDto>> BulkInsertAsync(IEnumerable<DailyFutureAuditDto> dto); 
        Task<DailyFutureAuditDto> UpdateAsync(DailyFutureAuditDto dto);
        Task<IEnumerable<DailyFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<DailyFutureAuditDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
