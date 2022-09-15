﻿using CSharpFunctionalExtensions;
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
    public class SchedulerSetupService : ApmaBaseService, ISchedulerSetupService
    {
        private readonly IMasterRepository _masterRepository; 

        public SchedulerSetupService(IMasterRepository masterRepository)
        {
            _masterRepository=masterRepository; 
        }
        public async Task<Result<PrometheusResponse>> InsertAsync(SchedulerSetupDto dto)
        {
            return new PrometheusResponse
            {
                Data = await _masterRepository.SchedulerSetupRepository.InsertAsync(dto)
            };
        }
        public async Task<Result<PrometheusResponse>> UpdateAsync(SchedulerSetupDto dto)
        {
            return new PrometheusResponse
            {
                Data = await _masterRepository.SchedulerSetupRepository.UpdateAsync(dto)
            };
        }
        public async Task<Result<PrometheusResponse>> SaveWithProcAsync(SchedulerSetupDto dto)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.SchedulerSetupRepository.SaveWithProcAsync(dto);

                    return PrometheusResponse.Success(resp, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        } 
        public async Task<IEnumerable<SchedulerSetupDto>> FindByAllScheduleAsync()
        {
           var result = await _masterRepository.SchedulerSetupRepository.FindByAllScheduleAsync();

            return result;
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
