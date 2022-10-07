using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;

namespace VDVI.AfasRepository.Interfaces
{
    public interface IDMFBeginbalansRepository
    {
        Task<DMFBeginbalansDto> InsertAsync(DMFBeginbalansDto dto);
        Task<IEnumerable<DMFBeginbalansDto>> BulkInsertAsync(IEnumerable<DMFBeginbalansDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<DMFBeginbalansDto> dto);
        Task<DMFBeginbalansDto> UpdateAsync(DMFBeginbalansDto dto); 
    }
}
