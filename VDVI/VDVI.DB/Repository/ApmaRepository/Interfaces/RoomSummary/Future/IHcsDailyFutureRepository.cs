using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsDailyFutureRepository
    {
        Task<DailyFutureDto> InsertAsync(DailyFutureDto dto);
        Task<IEnumerable<DailyFutureDto>> BulkInsertAsync(IEnumerable<DailyFutureDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<DailyFutureDto> dto);
        Task<DailyFutureDto> UpdateAsync(DailyFutureDto dto);
        Task<IEnumerable<DailyFutureDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<DailyFutureDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
