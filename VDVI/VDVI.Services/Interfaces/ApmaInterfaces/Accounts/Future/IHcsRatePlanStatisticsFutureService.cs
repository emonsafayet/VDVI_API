using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.Services.Interfaces
{
    public interface IHcsRatePlanStatisticsFutureService
    {
        Task<Result<PrometheusResponse>> InsertAsync(RatePlanStatisticFutureDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<RatePlanStatisticFutureDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RatePlanStatisticFutureDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
