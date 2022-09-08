using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos.RoomSummary;

namespace VDVI.Repository.Repository.Interfaces
{
    public interface IRoomSummaryRepository
    {
        Task<RoomSummaryDto> InsertAsync(RoomSummaryDto dto);
        Task<List<RoomSummaryDto>> BulkInsertAsync(List<RoomSummaryDto> dto);
        Task<RoomSummaryDto> UpdateAsync(RoomSummaryDto dto);
        Task<List<RoomSummaryDto>> FindAllByPropertyCodeAsync(string propertyCode);
        Task<RoomSummaryDto> FindByIdAsync(int id);
    }
}
