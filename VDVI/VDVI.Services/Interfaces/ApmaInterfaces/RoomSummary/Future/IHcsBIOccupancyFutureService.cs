using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBIOccupancyFutureService
    {
        Task<Result<PrometheusResponse>> InsertAsync(OccupancyFutureDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<OccupancyFutureDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<OccupancyFutureDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
