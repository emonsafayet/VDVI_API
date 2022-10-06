using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Models.AfasModels.Dto;

namespace VDVI.Services.Interfaces.APMA
{
    public interface IAfasSchedulerLogService
    {
        Task<Result<PrometheusResponse>> InsertAsync(AfasSchedulerLogDto dto);
        Task<Result<PrometheusResponse>> UpdateAsync(AfasSchedulerLogDto dto);
        Task<Result<PrometheusResponse>> SaveWithProcAsync(string methodName);
        Task<Result<PrometheusResponse>> FindByMethodNameAsync(string methodName);
        Task<Result<PrometheusResponse>> FindByIdAsync(string schedulerName);
        Task<bool> DeleteByPropertyCodeAsync(string schedulerName);
    }
}
