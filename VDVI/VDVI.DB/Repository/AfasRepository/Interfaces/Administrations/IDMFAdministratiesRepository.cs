using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.AfasRepository.Interfaces
{
    public interface IDMFAdministratiesRepository
    {
        Task<DMFAdministratiesDto> InsertAsync(DMFAdministratiesDto dto);
        Task<IEnumerable<DMFAdministratiesDto>> BulkInsertAsync(IEnumerable<DMFAdministratiesDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<DMFAdministratiesDto> dto);
        Task<DMFAdministratiesDto> UpdateAsync(DMFAdministratiesDto dto); 
        Task<DMFAdministratiesDto> FindByCodeAsync(string code); 
    }
}
