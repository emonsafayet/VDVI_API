using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;

namespace VDVI.AfasRepository.Interfaces
{
    public interface IDMFGrootboekrekeningenRepository
    {
        Task<DMFGrootboekrekeningenDto> InsertAsync(DMFGrootboekrekeningenDto dto);
        Task<IEnumerable<DMFGrootboekrekeningenDto>> BulkInsertAsync(IEnumerable<DMFGrootboekrekeningenDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<DMFGrootboekrekeningenDto> dto);
        Task<DMFGrootboekrekeningenDto> UpdateAsync(DMFGrootboekrekeningenDto dto); 
    }
}
