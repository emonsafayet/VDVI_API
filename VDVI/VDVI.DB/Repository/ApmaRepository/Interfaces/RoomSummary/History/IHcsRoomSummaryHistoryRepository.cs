using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsRoomSummaryHistoryRepository
    {
        Task<RoomSummaryHistoryDto> InsertAsync(RoomSummaryHistoryDto dto);
        Task<IEnumerable<RoomSummaryHistoryDto>> BulkInsertAsync(IEnumerable<RoomSummaryHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RoomSummaryHistoryDto> dto);
        Task<RoomSummaryHistoryDto> UpdateAsync(RoomSummaryHistoryDto dto);
        Task<IEnumerable<RoomSummaryHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RoomSummaryHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
