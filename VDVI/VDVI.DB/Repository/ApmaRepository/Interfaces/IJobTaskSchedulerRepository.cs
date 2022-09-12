using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.DB.IRepository
{
    public interface IJobTaskSchedulerRepository
    {
        Task<string> SaveWithProcAsync(JobTaskSchedulerDto dto);
        Task<string> FindByMethodNameAsync(string methodName);
    }
}
