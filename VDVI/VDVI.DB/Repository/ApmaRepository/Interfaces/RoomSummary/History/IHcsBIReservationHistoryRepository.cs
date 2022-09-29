using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIReservationHistoryRepository
    {
        Task<ReservationHistoryDto> InsertAsync(ReservationHistoryDto dto);
        Task<IEnumerable<ReservationHistoryDto>> BulkInsertAsync(IEnumerable<ReservationHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationHistoryDto> dto);
        Task<ReservationHistoryDto> UpdateAsync(ReservationHistoryDto dto);
        Task<IEnumerable<ReservationHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<ReservationHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
