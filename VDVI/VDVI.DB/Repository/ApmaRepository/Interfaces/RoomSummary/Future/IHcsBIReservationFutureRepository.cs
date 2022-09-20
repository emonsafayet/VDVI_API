using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIReservationFutureRepository
    {
        Task<ReservationFutureDto> InsertAsync(ReservationFutureDto dto);
        Task<IEnumerable<ReservationFutureDto>> BulkInsertAsync(IEnumerable<ReservationFutureDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationFutureDto> dto);
        Task<ReservationFutureDto> UpdateAsync(ReservationFutureDto dto);
        Task<IEnumerable<ReservationFutureDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<ReservationFutureDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
