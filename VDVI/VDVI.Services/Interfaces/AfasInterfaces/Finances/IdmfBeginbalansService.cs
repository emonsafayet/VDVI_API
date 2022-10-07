using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;

namespace VDVI.Services.AfasInterfaces
{
    public interface IdmfBeginbalansService
    {
        Task<Result<PrometheusResponse>> InsertAsync(DMFBeginbalansDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<DMFBeginbalansDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<DMFBeginbalansDto> dto);
    }
}
