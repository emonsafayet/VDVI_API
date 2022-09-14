using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface ISchedulerLogRepository
    { 
        Task<SchedulerLogDto> InsertAsync(SchedulerLogDto dto);
        Task<IEnumerable<SchedulerLogDto>> BulkInsertAsync(IEnumerable<SchedulerLogDto> dto);
        Task<SchedulerLogDto> UpdateAsync(SchedulerLogDto dto);
        Task<IEnumerable<SchedulerLogDto>> GetAllByPropertyCodeAsync(string propertyCode);
        Task<SchedulerLogDto> FindByIdAsync(int id);
       
    }
}
