using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.AfasRepository;
using VDVI.Repository.AfasDtos;
using VDVI.Services.AfasInterfaces;

namespace VDVI.Services.AfasServices
{ 
    public class DMFGrootboekrekeningenService : IdmfGrootboekrekeningenService
    {
        private readonly IAfasMasterRepositroy _masterRepository;
        public DMFGrootboekrekeningenService(IAfasMasterRepositroy masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<Result<PrometheusResponse>> BulkInsertAsync(List<DMFGrootboekrekeningenDto> dtos)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.DMFGrootboekrekeningenRepository.BulkInsertAsync(dtos);

                    return PrometheusResponse.Success(resp, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {exception.GetExceptionDetailMessage()}"),
                    RethrowException = false
                });
        }
        public async Task<Result<PrometheusResponse>> BulkInsertWithProcAsync(List<DMFGrootboekrekeningenDto> dtos)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var resp = await _masterRepository.DMFGrootboekrekeningenRepository.BulkInsertWithProcAsync(dtos);

                    return PrometheusResponse.Success(resp, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {exception.GetExceptionDetailMessage()}"),
                    RethrowException = false
                });
        }
        public async Task<Result<PrometheusResponse>> InsertAsync(DMFGrootboekrekeningenDto dto)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    dto = await _masterRepository.DMFGrootboekrekeningenRepository.InsertAsync(dto);

                    return PrometheusResponse.Success(dto, "Data saved successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {exception.GetExceptionDetailMessage()}"),
                    RethrowException = false
                });
        }
    }
}
