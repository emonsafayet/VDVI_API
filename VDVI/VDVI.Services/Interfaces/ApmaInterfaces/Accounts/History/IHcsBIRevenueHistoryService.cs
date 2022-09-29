using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos; 

namespace VDVI.Services.Interfaces
{
    public interface IHcsBIRevenueHistoryService
    {
        Task<Result<PrometheusResponse>> InsertAsync(RevenueHistoryDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<RevenueHistoryDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RevenueHistoryDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
