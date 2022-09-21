using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRatePlanStatisticsFutureAuditRepository
    {
        Task<RatePlanStatisticFutureAuditDto> InsertAsync(RatePlanStatisticFutureAuditDto dto);
        Task<IEnumerable<RatePlanStatisticFutureAuditDto>> BulkInsertAsync(IEnumerable<RatePlanStatisticFutureAuditDto> dto);
        Task<RatePlanStatisticFutureAuditDto> UpdateAsync(RatePlanStatisticFutureAuditDto dto);
        Task<IEnumerable<RatePlanStatisticFutureAuditDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RatePlanStatisticFutureAuditDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
