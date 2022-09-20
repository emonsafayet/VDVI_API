using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIOccupancyFutureRepository
    {
        Task<OccupancyFutureDto> InsertAsync(OccupancyFutureDto dto);
        Task<IEnumerable<OccupancyFutureDto>> BulkInsertAsync(IEnumerable<OccupancyFutureDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<OccupancyFutureDto> dto);
        Task<OccupancyFutureDto> UpdateAsync(OccupancyFutureDto dto);
        Task<IEnumerable<OccupancyFutureDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<OccupancyFutureDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
