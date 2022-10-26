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

namespace VDVI.Services.AfasServices
{
    public class DmfBoekingsdagenMutatesService : AfasBaseService, IdmfBoekingsdagenMutatiesService
    {
        private readonly IdmFBoekingsdagenMutationService _dmFBoekingsdagenMutationService;

        DMFBoekingsdagenMutatiesDto[]? AA;
        DMFBoekingsdagenMutatiesDto[]? AC;
        DMFBoekingsdagenMutatiesDto[]? AD;
        DMFBoekingsdagenMutatiesDto[]? AE;

        public DmfBoekingsdagenMutatesService(IdmFBoekingsdagenMutationService dmFBoekingsdagenMutationService)
        {
            _dmFBoekingsdagenMutationService = dmFBoekingsdagenMutationService;
        } 
       
        public async Task<Result<PrometheusResponse>> DmfBoekingsdagenMutatiesServiceAsync()
        {   
            var Dto = new List<DMFBoekingsdagenMutatiesDto>();
            AfasCrenditalsDto getConnector = GetAfmaConnectors();

            var res = await _dmFBoekingsdagenMutationService.GetInitialRecordAndLastRecordDatetime();
            var formatres = (MutationDto)res.Value.Data; 

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    //Load Initial Records  
                    if (formatres.IsInitialRecord == false) await LoadAsync(getConnector);
                   
                    //Get Changes Record  
                    else
                    {
                        DateTime? dt = formatres.LastRecordDate;
                        string lastmutationdatetime = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dt); //check this date in the database 
                        string lastdayofthisyear = DateTime.Now.Year.ToString() + "-12-31T00:00:00.000"; //last day of current year

                        await LoadAsync(lastmutationdatetime, lastdayofthisyear, getConnector);
                       
                    }
                    //Format Data
                    FormatSummaryObject(AA.ToList(), Dto);
                    //FormatSummaryObject(AA.ToList(),AC.ToList(),AD.ToList(),AE.ToList(), Dto);


                    // DB operation
                    if (Dto.Count > 0)  await _dmFBoekingsdagenMutationService.BulkInsertWithProcAsync(Dto);
                    

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );

        }
        public async Task LoadAsync(AfasCrenditalsDto getConnector)
        {
            AA = await getConnector.clientAA.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();
            //AC = await getConnector.clientAC.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();
            //AD = await getConnector.clientAD.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();
            //AE = await getConnector.clientAE.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();
        }
        public async Task LoadAsync(string lastmutationdatetime, string lastdayofthisyear, AfasCrenditalsDto getConnector)
        {


            AA = await getConnector.clientAA.Query<DMFBoekingsdagenMutatiesDto>().WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                            .WhereLessOrEqual(x => x.Datum_gewijzigd, lastdayofthisyear).Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();

            /*
            AC = await getConnector.clientAC.Query<DMFBoekingsdagenMutatiesDto>().WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                           .WhereLessOrEqual(x => x.Datum_gewijzigd, lastdayofthisyear).Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();

            AD = await getConnector.clientAD.Query<DMFBoekingsdagenMutatiesDto>().WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                           .WhereLessOrEqual(x => x.Datum_gewijzigd, lastdayofthisyear).Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();

            AE = await getConnector.clientAE.Query<DMFBoekingsdagenMutatiesDto>().WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                           .WhereLessOrEqual(x => x.Datum_gewijzigd, lastdayofthisyear).Skip(-1).Take(-1).OrderBy(x => x.Datum_gewijzigd).GetAsync();
             */

        }

        //private void FormatSummaryObject(List<DMFBoekingsdagenMutatiesDto> _AA,List<DMFBoekingsdagenMutatiesDto> _AC,List<DMFBoekingsdagenMutatiesDto> _AD,List<DMFBoekingsdagenMutatiesDto> _AE, List<DMFBoekingsdagenMutatiesDto> _Dto)
        private void FormatSummaryObject(List<DMFBoekingsdagenMutatiesDto> _AA, List<DMFBoekingsdagenMutatiesDto> _Dto)

        {
            _AA.ForEach(a => a.Omgeving_code = "AA");
            _Dto.AddRange(_AA);

        /*
            _AC.ForEach(a => a.Omgeving_code = "AC");
            _Dto.AddRange(_AC);

            _AD.ForEach(a => a.Omgeving_code = "AD");
            _Dto.AddRange(_AD);

            _AE.ForEach(a => a.Omgeving_code = "AE");
            _Dto.AddRange(_AE);
        */
        }
    }
}
