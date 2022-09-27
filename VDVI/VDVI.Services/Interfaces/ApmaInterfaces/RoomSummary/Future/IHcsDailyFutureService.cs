
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos; 

namespace VDVI.Services.Interfaces
{
    public interface IHcsDailyFutureService
    {
        Task<Result<PrometheusResponse>> InsertAsync(DailyFutureDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<DailyFutureDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<DailyFutureDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
