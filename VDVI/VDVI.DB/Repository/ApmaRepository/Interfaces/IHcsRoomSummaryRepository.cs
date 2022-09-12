using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.Repository.Interfaces
{
    public interface IHcsRoomSummaryRepository
    {
        Task<RoomSummaryDto> InsertAsync(RoomSummaryDto dto);
        Task<IEnumerable<RoomSummaryDto>> BulkInsertAsync(IEnumerable<RoomSummaryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RoomSummaryDto> dto);
        Task<RoomSummaryDto> UpdateAsync(RoomSummaryDto dto);
        Task<IEnumerable<RoomSummaryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RoomSummaryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
