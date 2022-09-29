using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRevenueFutureRepository
    {
        Task<ReservationDashboardRevenueFutureDto> InsertAsync(ReservationDashboardRevenueFutureDto dto);
        Task<IEnumerable<ReservationDashboardRevenueFutureDto>> BulkInsertAsync(IEnumerable<ReservationDashboardRevenueFutureDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDashboardRevenueFutureDto> dto);
        Task<ReservationDashboardRevenueFutureDto> UpdateAsync(ReservationDashboardRevenueFutureDto dto);
        Task<IEnumerable<ReservationDashboardRevenueFutureDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<ReservationDashboardRevenueFutureDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
