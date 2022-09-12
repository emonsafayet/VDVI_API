using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Repository.Interfaces
{
    public interface IHcsBIRevenueRepository
    {
        Task<RevenueDto> InsertAsync(RevenueDto dto);
        Task<IEnumerable<RevenueDto>> BulkInsertAsync(IEnumerable<RevenueDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RevenueDto> dto);
        Task<RevenueDto> UpdateAsync(RevenueDto dto);
        Task<IEnumerable<RevenueDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RevenueDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
