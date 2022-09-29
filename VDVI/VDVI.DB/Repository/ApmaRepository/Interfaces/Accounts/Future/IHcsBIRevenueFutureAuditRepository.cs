using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRevenueFutureAuditRepository
    {
        Task<ReservationDashboardRevenueFutureAuditDto> InsertAsync(ReservationDashboardRevenueFutureAuditDto dto);
        Task<IEnumerable<ReservationDashboardRevenueFutureAuditDto>> BulkInsertAsync(IEnumerable<ReservationDashboardRevenueFutureAuditDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDashboardRevenueFutureAuditDto> dto);
        Task<ReservationDashboardRevenueFutureAuditDto> UpdateAsync(ReservationDashboardRevenueFutureAuditDto dto);
        Task<IEnumerable<ReservationDashboardRevenueFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<ReservationDashboardRevenueFutureAuditDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
