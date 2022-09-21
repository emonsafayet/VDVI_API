using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRevenueFutureRepository
    {
        Task<RevenueFutureDto> InsertAsync(RevenueFutureDto dto);
        Task<IEnumerable<RevenueFutureDto>> BulkInsertAsync(IEnumerable<RevenueFutureDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RevenueFutureDto> dto);
        Task<RevenueFutureDto> UpdateAsync(RevenueFutureDto dto);
        Task<IEnumerable<RevenueFutureDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RevenueFutureDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
