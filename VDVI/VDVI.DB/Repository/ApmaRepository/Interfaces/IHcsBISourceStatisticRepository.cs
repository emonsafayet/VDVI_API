using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.Repository.Interfaces
{
    public interface IHcsBISourceStatisticRepository
    {
        Task<SourceStatisticDto> InsertAsync(SourceStatisticDto dto);
        Task<IEnumerable<SourceStatisticDto>> BulkInsertAsync(IEnumerable<SourceStatisticDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<SourceStatisticDto> dto);
        Task<SourceStatisticDto> UpdateAsync(SourceStatisticDto dto);
        Task<IEnumerable<SourceStatisticDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<SourceStatisticDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
