using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.Repository.Interfaces
{
    public interface IHcsBIRatePlanStatisticRepository
    {
        Task<RatePlanStatisticDto> InsertAsync(RatePlanStatisticDto dto);
        Task<IEnumerable<RatePlanStatisticDto>> BulkInsertAsync(IEnumerable<RatePlanStatisticDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<RatePlanStatisticDto> dto);
        Task<RatePlanStatisticDto> UpdateAsync(RatePlanStatisticDto dto);
        Task<IEnumerable<RatePlanStatisticDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<RatePlanStatisticDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
