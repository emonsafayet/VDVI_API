using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.RoomSummary;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBIRoomsFutureService
    {
        Task<Result<PrometheusResponse>> InsertAsync(RoomsFutureDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<RoomsFutureDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RoomsFutureDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
