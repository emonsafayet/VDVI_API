using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.ApmaDtos.Common;

namespace VDVI.ApmaRepository.Interfaces
{
    public interface ISchedulerSetupRepository
    {
        Task<string> SaveWithProcAsync(SchedulerSetupDto dto);
        Task<DbSchedulerSetup> FindByMethodNameAsync(string methodName);
        Task<SchedulerSetupDto> UpdateAsync(SchedulerSetupDto dto);
    }
}
