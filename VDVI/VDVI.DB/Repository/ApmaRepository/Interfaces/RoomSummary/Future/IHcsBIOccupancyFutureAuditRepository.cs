using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIOccupancyFutureAuditRepository
    {
        Task<OccupancyFutureAuditDto> InsertAsync(OccupancyFutureAuditDto dto);
        Task<IEnumerable<OccupancyFutureAuditDto>> BulkInsertAsync(IEnumerable<OccupancyFutureAuditDto> dto);
        Task<OccupancyFutureAuditDto> UpdateAsync(OccupancyFutureAuditDto dto);
        Task<IEnumerable<OccupancyFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<OccupancyFutureAuditDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
