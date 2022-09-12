using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.Interfaces
{
    public interface IHcsBIReservationRepository
    {
        Task<ReservationDto> InsertAsync(ReservationDto dto);
        Task<IEnumerable<ReservationDto>> BulkInsertAsync(IEnumerable<ReservationDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDto> dto);
        Task<ReservationDto> UpdateAsync(ReservationDto dto);
        Task<IEnumerable<ReservationDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<ReservationDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
