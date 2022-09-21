using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRoomsFutureAuditRepository
    {
        Task<RoomsFutureAuditDto> InsertAsync(RoomsFutureAuditDto dto);
        Task<IEnumerable<RoomsFutureAuditDto>> BulkInsertAsync(IEnumerable<RoomsFutureAuditDto> dto);
        Task<RoomsFutureAuditDto> UpdateAsync(RoomsFutureAuditDto dto);
        Task<IEnumerable<RoomsFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RoomsFutureAuditDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
