using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRevenueHistoryRepository
    {
        Task<ReservationDashboardRevenueHistoryDto> InsertAsync(ReservationDashboardRevenueHistoryDto dto);
        Task<IEnumerable<ReservationDashboardRevenueHistoryDto>> BulkInsertAsync(IEnumerable<ReservationDashboardRevenueHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<ReservationDashboardRevenueHistoryDto> dto);
        Task<ReservationDashboardRevenueHistoryDto> UpdateAsync(ReservationDashboardRevenueHistoryDto dto);
        Task<IEnumerable<ReservationDashboardRevenueHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<ReservationDashboardRevenueHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
