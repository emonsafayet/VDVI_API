using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.Interfaces
{
    public interface IHcsBIRoomsRepository
    {
        Task<RoomsDto> InsertAsync(RoomsDto dto);
        Task<IEnumerable<RoomsDto>> BulkInsertAsync(IEnumerable<RoomsDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RoomsDto> dto);
        Task<RoomsDto> UpdateAsync(RoomsDto dto);
        Task<IEnumerable<RoomsDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RoomsDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
