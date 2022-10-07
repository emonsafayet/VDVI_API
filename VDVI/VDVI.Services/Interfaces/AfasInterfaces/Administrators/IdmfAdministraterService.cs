using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;

namespace VDVI.Services.Interfaces.AfasInterfaces.Administrators
{
    public interface IdmfAdministraterService
    {
        Task<Result<PrometheusResponse>> InsertAsync(DMFAdministratiesDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<DMFAdministratiesDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<DMFAdministratiesDto> dto);
    }
}
