﻿using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.ApmaRepository;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Services.Interfaces;

namespace VDVI.Services
{
    public class HcsBIRevenueFutureService: IHcsBIRevenueFutureService
    {
        private readonly IMasterRepository _masterRepository;
        public HcsBIRevenueFutureService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<Result<PrometheusResponse>> BulkInsertAsync(List<RevenueFutureDto> dtos)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.HcsBIRevenueFutureRepository.BulkInsertAsync(dtos);

                    return PrometheusResponse.Success(resp, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RevenueFutureDto> dtos)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.HcsBIRevenueFutureRepository.BulkInsertWithProcAsync(dtos);

                    return PrometheusResponse.Success(resp, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var dbroomSummariesRes = await _masterRepository.HcsBIRevenueFutureRepository.DeleteByPropertyCodeAsync(propertyCode);

                    return PrometheusResponse.Success("", "Data delete is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var dtos = await _masterRepository.HcsBIRevenueFutureRepository.GetAllByPropertyCodeAsync(propertyCode);

                    return PrometheusResponse.Success(dtos, "Data retrival successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> InsertAsync(RevenueFutureDto dto)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    dto = await _masterRepository.HcsBIRevenueFutureRepository.InsertAsync(dto);

                    return PrometheusResponse.Success(dto, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.HcsBIRevenueFutureRepository.DeleteByDashboardDateAsync(businessDate);

                    return PrometheusResponse.Success("", "Data removal is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }
    }
}
