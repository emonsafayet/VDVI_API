using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;

namespace VDVI.AfasRepository.Interfaces
{
    public interface IDMFFinancieleMutatiesRepository
    {
        Task<DMFFinancieleMutatiesDto> InsertAsync(DMFFinancieleMutatiesDto dto);
        Task<IEnumerable<DMFFinancieleMutatiesDto>> BulkInsertAsync(IEnumerable<DMFFinancieleMutatiesDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<DMFFinancieleMutatiesDto> dto);
        Task<DMFFinancieleMutatiesDto> UpdateAsync(DMFFinancieleMutatiesDto dto); 
    }
}
