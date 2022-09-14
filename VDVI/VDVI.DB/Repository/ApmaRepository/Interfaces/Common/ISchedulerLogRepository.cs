using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.ApmaDtos.Common;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface ISchedulerLogRepository
    {
        Task<SchedulerLogDto> InsertAsync(SchedulerLogDto dto);
    }
}
