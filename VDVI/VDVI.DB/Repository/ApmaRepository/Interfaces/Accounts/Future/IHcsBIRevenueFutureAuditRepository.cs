using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRevenueFutureAuditRepository
    {
        Task<RevenueFutureAuditDto> InsertAsync(RevenueFutureAuditDto dto);
        Task<IEnumerable<RevenueFutureAuditDto>> BulkInsertAsync(IEnumerable<RevenueFutureAuditDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RevenueFutureAuditDto> dto);
        Task<RevenueFutureAuditDto> UpdateAsync(RevenueFutureAuditDto dto);
        Task<IEnumerable<RevenueFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RevenueFutureAuditDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByDashboardDateAsync(DateTime DashboardDate);
    }
}
