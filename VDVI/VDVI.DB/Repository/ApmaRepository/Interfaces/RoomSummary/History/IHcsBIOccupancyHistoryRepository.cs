using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIOccupancyHistoryRepository
    {
        Task<OccupancyHistoryDto> InsertAsync(OccupancyHistoryDto dto);
        Task<IEnumerable<OccupancyHistoryDto>> BulkInsertAsync(IEnumerable<OccupancyHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<OccupancyHistoryDto> dto);
        Task<OccupancyHistoryDto> UpdateAsync(OccupancyHistoryDto dto);
        Task<IEnumerable<OccupancyHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<OccupancyHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
