using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRoomsHistoryRepository
    {
        Task<RoomsHistoryDto> InsertAsync(RoomsHistoryDto dto);
        Task<IEnumerable<RoomsHistoryDto>> BulkInsertAsync(IEnumerable<RoomsHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RoomsHistoryDto> dto);
        Task<RoomsHistoryDto> UpdateAsync(RoomsHistoryDto dto);
        Task<IEnumerable<RoomsHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RoomsHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
