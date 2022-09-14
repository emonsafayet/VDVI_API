using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.Services.Interfaces
{
    public interface IHcsRatePlanStatisticsHistoryService
    {
        Task<Result<PrometheusResponse>> InsertAsync(RatePlanStatisticHistoryDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<RatePlanStatisticHistoryDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RatePlanStatisticHistoryDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
