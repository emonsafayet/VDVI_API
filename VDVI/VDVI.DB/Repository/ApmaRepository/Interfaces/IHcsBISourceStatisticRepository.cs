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
        Task<SourceStatisticHistoryDto> InsertAsync(SourceStatisticHistoryDto dto);
        Task<IEnumerable<SourceStatisticHistoryDto>> BulkInsertAsync(IEnumerable<SourceStatisticHistoryDto> dto);
        Task<string> BulkInsertWithProcAsync(IEnumerable<SourceStatisticHistoryDto> dto);
        Task<SourceStatisticHistoryDto> UpdateAsync(SourceStatisticHistoryDto dto);
        Task<IEnumerable<SourceStatisticHistoryDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<SourceStatisticHistoryDto> FindByIdAsync(int id);
        Task<bool> DeleteByPropertyCodeAsync(string propertyCode);
        Task<bool> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
