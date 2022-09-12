using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.Interfaces
{
    public interface IHcsBIOccupancyRepository
    {
        Task<OccupancyDto> InsertAsync(OccupancyDto dto);
        Task<IEnumerable<OccupancyDto>> BulkInsertAsync(IEnumerable<OccupancyDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<OccupancyDto> dto);
        Task<OccupancyDto> UpdateAsync(OccupancyDto dto);
        Task<IEnumerable<OccupancyDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<OccupancyDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
