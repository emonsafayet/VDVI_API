using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBIRevenueFutureService
    {
        Task<Result<PrometheusResponse>> InsertAsync(RevenueFutureDto dto);
        Task<Result<PrometheusResponse>> BulkInsertAsync(List<RevenueFutureDto> dtos);
        Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<RevenueFutureDto> dtos);
        Task<Result<PrometheusResponse>> GetByPropertCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByPropertyCodeAsync(string propertyCode);
        Task<Result<PrometheusResponse>> DeleteByBusinessDateAsync(DateTime businessDate);
    }
}
