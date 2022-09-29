using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.Services.Interfaces
{
    public interface ISchedulerLogService
    {
        Task<Result<PrometheusResponse>> InsertAsync(SchedulerLogDto dto);
        Task<Result<PrometheusResponse>> UpdateAsync(SchedulerLogDto dto);
        Task<Result<PrometheusResponse>> SaveWithProcAsync(string methodName); 
        Task<Result<PrometheusResponse>> FindByMethodNameAsync(string methodName);
        Task<Result<PrometheusResponse>> FindByIdAsync(string schedulerName);
        Task<bool> DeleteByPropertyCodeAsync(string schedulerName);
    }
}
