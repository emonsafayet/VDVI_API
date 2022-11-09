using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.AfasDtos;
using VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Services.BaseService;
using DutchGrit.Afas;
using Framework.Core.Extensions;

namespace VDVI.Services.AfasServices
{
    public class DmfBoekingsdagenMutatesService : AfasBaseService, IdmfBoekingsdagenMutatiesService
    {
        private readonly IdmFBoekingsdagenMutationService _dmFBoekingsdagenMutationService;

        public DmfBoekingsdagenMutatesService(IdmFBoekingsdagenMutationService dmFBoekingsdagenMutationService)
        {
            _dmFBoekingsdagenMutationService = dmFBoekingsdagenMutationService;
        }

        public async Task<Result<PrometheusResponse>> DmfBoekingsdagenMutatiesServiceAsync()
        {
            AfasCrenditalsDto getConnector = GetAfmaConnectors();
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {

                    var currentDate = DateTime.UtcNow.Date;

                    List<DMFBoekingsdagenMutatiesDto> DtoList = new List<DMFBoekingsdagenMutatiesDto>();
                    DtoList.AddRange(await GetBoekingsdagenMutatiesAsync(getConnector.clientAA, p => p.Datum_boeking <= currentDate, "AA"));
                    DtoList.AddRange(await GetBoekingsdagenMutatiesAsync(getConnector.clientAC, p => p.Datum_boeking <= currentDate, "AC"));
                    DtoList.AddRange(await GetBoekingsdagenMutatiesAsync(getConnector.clientAD, p => p.Datum_boeking <= currentDate, "AD"));
                    DtoList.AddRange(await GetBoekingsdagenMutatiesAsync(getConnector.clientAE, p => p.Datum_boeking <= currentDate, "AE"));

                   
                    // DB operation
                    if (DtoList.Count > 0)
                    {
                        await _dmFBoekingsdagenMutationService.BulkInsertWithProcAsync(DtoList, true);
                        DtoList.Clear();
                    }


                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );

        }

        private async Task<List<DMFBoekingsdagenMutatiesDto>> GetBoekingsdagenMutatiesAsync(AfasClient Client, Func<DMFBoekingsdagenMutatiesDto, bool> predicate, string environmentCode)
        {
            var result = (await Client.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).GetAsync()).ToList().Where(predicate).ToList();
            if (result.Count > 0)
                return result.FormatList(x => x.Omgeving_code, environmentCode);

            return new List<DMFBoekingsdagenMutatiesDto>();
        }
    }
}
