using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;

namespace VDVI.Services.AfasInterfaces
{
    public interface IdmfGrootboekrekeningenService
    {
        Task<Result<PrometheusResponse>> InsertAsync(DMFGrootboekrekeningenDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<DMFGrootboekrekeningenDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<DMFGrootboekrekeningenDto> dto);
    }
}
