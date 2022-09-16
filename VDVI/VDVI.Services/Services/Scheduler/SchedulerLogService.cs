using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.ApmaRepository;
using VDVI.ApmaRepository.Interfaces;
using VDVI.DB.Dtos;
using VDVI.Repository.ApmaRepository.Implementation;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using VDVI.Repository.Dtos.SourceStatistics;
using VDVI.Services.Interfaces;

namespace VDVI.Services
{
    public class SchedulerLogService : ApmaBaseService, ISchedulerLogService
    {
        private readonly IMasterRepository _masterRepository;

        public SchedulerLogService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<Result<PrometheusResponse>> InsertAsync(SchedulerLogDto dto)
        { 
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    await _masterRepository.SchedulerLogRepository.InsertAsync(dto);
                    return PrometheusResponse.Success("", "Data Insert is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }
        public async Task<Result<PrometheusResponse>> UpdateAsync(SchedulerLogDto dto)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                 async () =>
                 {
                     await _masterRepository.SchedulerLogRepository.UpdateAsync(dto);
                     return PrometheusResponse.Success("", "Data Update is successful");
                 },
                 exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                 {
                     DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                     RethrowException = false
                 }); 
        }
        public async Task<Result<PrometheusResponse>> SaveWithProcAsync(string methodName)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.SchedulerLogRepository.SaveWithProcAsync(methodName);

                    return PrometheusResponse.Success(resp, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }        
        public async Task<Result<PrometheusResponse>> FindByMethodNameAsync(string methodName)
        {
            throw new NotImplementedException();
        }
        public async Task<Result<PrometheusResponse>> FindByIdAsync(string schedulerName)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteByPropertyCodeAsync(string schedulerName)
        {
            throw new NotImplementedException();
        }

    }
}
