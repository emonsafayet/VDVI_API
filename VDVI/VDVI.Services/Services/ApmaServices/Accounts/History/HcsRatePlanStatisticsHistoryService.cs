using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Services.Interfaces;
using VDVI.ApmaRepository;

namespace VDVI.Services
{
    public class HcsRatePlanStatisticsHistoryService : IHcsRatePlanStatisticsHistoryService
    {

        private readonly IMasterRepository _masterRepository;
        public HcsRatePlanStatisticsHistoryService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<Result<PrometheusResponse>> BulkInsertAsync(List<RatePlanStatisticHistoryDto> dtos)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.HcsBIRatePlanStatisticsHistoryRepository.BulkInsertAsync(dtos);

                    return PrometheusResponse.Success(resp, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RatePlanStatisticHistoryDto> dtos)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.HcsBIRatePlanStatisticsHistoryRepository.BulkInsertWithProcAsync(dtos);

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
                    var dbroomSummariesRes = await _masterRepository.HcsBIRatePlanStatisticsHistoryRepository.DeleteByPropertyCodeAsync(propertyCode);

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
                    var dtos = await _masterRepository.HcsBIRatePlanStatisticsHistoryRepository.GetAllByPropertyCodeAsync(propertyCode);

                    return PrometheusResponse.Success(dtos, "Data retrival successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        public async Task<Result<PrometheusResponse>> InsertAsync(RatePlanStatisticHistoryDto dto)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    dto = await _masterRepository.HcsBIRatePlanStatisticsHistoryRepository.InsertAsync(dto);

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
                    var dbroomSummariesRes = await _masterRepository.HcsBIRatePlanStatisticsHistoryRepository.DeleteByBusinessDateAsync(businessDate);

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
