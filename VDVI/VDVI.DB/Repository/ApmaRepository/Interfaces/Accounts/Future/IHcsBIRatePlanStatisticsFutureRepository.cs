using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface IHcsBIRatePlanStatisticsFutureRepository
    {
        Task<RatePlanStatisticFutureDto> InsertAsync(RatePlanStatisticFutureDto dto);
        Task<IEnumerable<RatePlanStatisticFutureDto>> BulkInsertAsync(IEnumerable<RatePlanStatisticFutureDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RatePlanStatisticFutureDto> dto);
        Task<RatePlanStatisticFutureDto> UpdateAsync(RatePlanStatisticFutureDto dto);
        Task<IEnumerable<RatePlanStatisticFutureDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RatePlanStatisticFutureDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
