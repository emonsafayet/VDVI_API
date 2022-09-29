using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRoomsFutureRepository
    {
        Task<RoomsFutureDto> InsertAsync(RoomsFutureDto dto);
        Task<IEnumerable<RoomsFutureDto>> BulkInsertAsync(IEnumerable<RoomsFutureDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RoomsFutureDto> dto);
        Task<RoomsFutureDto> UpdateAsync(RoomsFutureDto dto);
        Task<IEnumerable<RoomsFutureDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RoomsFutureDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
