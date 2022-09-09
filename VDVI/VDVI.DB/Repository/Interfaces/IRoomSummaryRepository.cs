using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.Repository.Repository.Interfaces
{
    public interface IRoomSummaryRepository
    {
        Task<RoomSummaryDto> InsertAsync(RoomSummaryDto dto);
        Task<List<RoomSummaryDto>> BulkInsertAsync(List<RoomSummaryDto> dto);
        Task<string> BulkInsertWithProcAsync(List<RoomSummaryDto> dto);
        Task<RoomSummaryDto> UpdateAsync(RoomSummaryDto dto);
        Task<List<RoomSummaryDto>> GetByPropertyCodeAsync(string propertyCode);
        Task<RoomSummaryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
    }
}
