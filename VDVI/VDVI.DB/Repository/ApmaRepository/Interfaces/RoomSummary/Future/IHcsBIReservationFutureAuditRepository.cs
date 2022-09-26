using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIReservationFutureAuditRepository
    {
        Task<ReservationFutureAuditDto> InsertAsync(ReservationFutureAuditDto dto);
        Task<IEnumerable<ReservationFutureAuditDto>> BulkInsertAsync(IEnumerable<ReservationFutureAuditDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationFutureAuditDto> dto);
        Task<ReservationFutureAuditDto> UpdateAsync(ReservationFutureAuditDto dto);
        Task<IEnumerable<ReservationFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<ReservationFutureAuditDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
