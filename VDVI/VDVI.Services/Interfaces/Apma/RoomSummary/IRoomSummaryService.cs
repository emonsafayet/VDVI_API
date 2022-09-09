
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace VDVI.Services.Interfaces
{
    public interface IRoomSummaryService
    {
        Task<Result<PrometheusResponse>> InsertAsync(RoomSummaryDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<RoomSummaryDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RoomSummaryDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);

    }
}
