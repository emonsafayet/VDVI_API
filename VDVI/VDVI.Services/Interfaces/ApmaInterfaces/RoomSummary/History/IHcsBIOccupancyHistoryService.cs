using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.DB.Dtos;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBIOccupancyHistoryService
    {
        Task<Result<PrometheusResponse>> InsertAsync(OccupancyHistoryDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<OccupancyHistoryDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<OccupancyHistoryDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
