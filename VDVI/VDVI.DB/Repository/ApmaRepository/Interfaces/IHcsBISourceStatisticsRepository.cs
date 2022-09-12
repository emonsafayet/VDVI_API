using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.DB.IRepository
{
    public interface IHcsBISourceStatisticsRepository
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
