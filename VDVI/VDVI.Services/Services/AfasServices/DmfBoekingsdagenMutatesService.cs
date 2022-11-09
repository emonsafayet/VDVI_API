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
using VDVI.Repository.AfasDtos;

namespace VDVI.Services.AfasServices
{
    public class DmfBoekingsdagenMutatesService : AfasBaseService, IdmfBoekingsdagenMutatiesService
    {
        private readonly IdmFFinancieleMutationService _dmFFinancieleMutationService;
        private readonly IdmFBoekingsdagenMutationService _dmFBoekingsdagenMutationService;
        List<DMFFinancieleMutatiesDto> FinancieleMutatiesDto = new List<DMFFinancieleMutatiesDto>();
        List<DMFBoekingsdagenMutatiesDto> BoekingsdagenMutatieDtoList = new List<DMFBoekingsdagenMutatiesDto>();

        public DmfBoekingsdagenMutatesService
            (
                IdmFBoekingsdagenMutationService dmFBoekingsdagenMutationService,
                  IdmFFinancieleMutationService dmFFinancieleMutationService,
                AfasCrenditalsDto afasCrenditalsDto
            ) : base(afasCrenditalsDto)
        {
            _dmFFinancieleMutationService = dmFFinancieleMutationService;
            _dmFBoekingsdagenMutationService = dmFBoekingsdagenMutationService;
        }

        public async Task<Result<PrometheusResponse>> DmfBoekingsdagenMutatiesServiceAsync()
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {

                    var currentDate = DateTime.UtcNow.Date;

                    BoekingsdagenMutatieDtoList.AddRange(await GetBoekingsdagenMutatiesAsync(AfasClients.clientAA, p => p.Datum_boeking <= currentDate, "AA"));
                    BoekingsdagenMutatieDtoList.AddRange(await GetBoekingsdagenMutatiesAsync(AfasClients.clientAC, p => p.Datum_boeking <= currentDate, "AC"));
                    BoekingsdagenMutatieDtoList.AddRange(await GetBoekingsdagenMutatiesAsync(AfasClients.clientAD, p => p.Datum_boeking <= currentDate, "AD"));
                    BoekingsdagenMutatieDtoList.AddRange(await GetBoekingsdagenMutatiesAsync(AfasClients.clientAE, p => p.Datum_boeking <= currentDate, "AE"));

                    var datelist = BoekingsdagenMutatieDtoList.Select(x=>x.Datum_boeking).Distinct().ToList();
                
                    if (datelist.Count > 0)
                    {
                            FinancieleMutatiesDto.AddRange(await GetDMFFinancieleMutatiesAsync(AfasClients.clientAA, p => true,
                                                    datelist,  "AA"));
                            FinancieleMutatiesDto.AddRange(await GetDMFFinancieleMutatiesAsync(AfasClients.clientAC, p => true,
                                                  datelist, "AC"));
                            FinancieleMutatiesDto.AddRange(await GetDMFFinancieleMutatiesAsync(AfasClients.clientAD, p => true,
                                                    datelist, "AD"));
                            FinancieleMutatiesDto.AddRange(await GetDMFFinancieleMutatiesAsync(AfasClients.clientAE, p => true,
                                                   datelist, "AE")); 
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

        private async Task<List<DMFBoekingsdagenMutatiesDto>> GetBoekingsdagenMutatiesAsync(AfasClient Client, 
                                    Func<DMFBoekingsdagenMutatiesDto, bool> predicate, string environmentCode)
        {
            var result = (await Client.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).GetAsync()).Where(predicate).ToList();
            if (result.Count > 0)
                return result.FormatList(x => x.Omgeving_code, environmentCode);

            return new List<DMFBoekingsdagenMutatiesDto>();
        }
        private async Task<List<DMFFinancieleMutatiesDto>> GetDMFFinancieleMutatiesAsync(AfasClient Client, Func<DMFFinancieleMutatiesDto, bool> predicate, 
                                      List<DateTime?> Listyear, string environmentCode)
        {
            List<DMFFinancieleMutatiesDto> result = new List<DMFFinancieleMutatiesDto>();

            //for (int i = 1; i <= Listyear.Count; i++)
            //{
            foreach (var item in Listyear)
            { 
                //var datebooking = Listyear.Skip(pageIndex * pageSize).Take(pageSize);

                var tempResult = (await Client.Query<DMFFinancieleMutatiesDto>()
                        .WhereEquals(x => x.Datum_boeking,  item.ToString())
                        .Skip(-1).Take(-1).OrderBy(x => x.Rekeningnummer).GetAsync()).Where(predicate).ToList();
                 

                    tempResult.FormatList(x => x.Omgeving_code, environmentCode);

                if (tempResult.Count > 0) 
                    await _dmFBoekingsdagenMutationService.BulkInsertWithProcAsync(BoekingsdagenMutatieDtoList,FinancieleMutatiesDto); 


            }

            return result;
        }

    }
}
