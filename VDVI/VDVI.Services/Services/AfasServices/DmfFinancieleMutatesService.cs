using CSharpFunctionalExtensions; 
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility; 
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos; 
using VDVI.Repository.Dtos.AfasDtos; 
using VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos; 
using VDVI.Services.AfasInterfaces; 
using VDVI.Services.Services.BaseService;

namespace VDVI.Services.AfasServices
{
    public class DmfFinancieleMutatesService : AfasBaseService, IdmfFinancieleMutatiesService
    {
        private readonly IdmFFinancieleMutationService _dmFFinancieleMutationService;
        List<DMFAdministratiesDto> administratiesdto = new List<DMFAdministratiesDto>(); 

        DMFFinancieleMutatiesDto[]? financielemutatiesAA;
        DMFFinancieleMutatiesDto[]? financielemutatiesAC;
        DMFFinancieleMutatiesDto[]? financielemutatiesAD;
        DMFFinancieleMutatiesDto[]? financielemutatiesAE; 

        public DmfFinancieleMutatesService(IdmFFinancieleMutationService dmFFinancieleMutationService)
        {
            _dmFFinancieleMutationService = dmFFinancieleMutationService;
        }


        public async Task DmfFinancieleMutatiesServiceInitial(int year, int administratie_code, AfasCrenditalsDto getConnector)
        {


            financielemutatiesAA = await getConnector.clientAA.Query<DMFFinancieleMutatiesDto>()
                                                                    .WhereEquals(x => x.Administratie_code, administratie_code.ToString())
                                                                    .WhereEquals(x => x.Jaar, year.ToString())
                                                                    .Skip(-1)
                                                                    .Take(-1)
                                                                    .OrderBy(x => x.Administratie_code)
                                                                    .OrderBy(x => x.Journaalpost_nr)
                                                                    .OrderBy(x => x.Journaalpost_vnr)
                                                                    .OrderBy(x => x.Journaalpost_vnr_vb)
                                                                    .GetAsync();



        } 
        public async Task DmfFinancieleMutatiesServiceExistingAsync(string lastmutationdatetime, string lastdayofthisyear, AfasCrenditalsDto getConnector)
        {
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAA;
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAC;
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAD;
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAE; 
            boekingsdagenMutatiesAA = await getConnector.clientAA.Query<DMFBoekingsdagenMutatiesDto>()
                                            .WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                            .WhereLessOrEqual(x => x.Datum_boeking, lastdayofthisyear)
                                            .Skip(-1) //skip none
                                            .Take(-1) //take all
                                            .OrderBy(x => x.Datum_boeking)
                                            .GetAsync();
            if (boekingsdagenMutatiesAA.Length>0)
            {
                foreach (var item in boekingsdagenMutatiesAA)
                {
                    DateTime dt = item.Datum_boeking.Value;
                    string bookingdate = String.Format("{0:yyyy-MM-ddT00:00:00.000}", dt);

                    //get financial mutations for selected booking date
                    financielemutatiesAA = await getConnector.clientAA.Query<DMFFinancieleMutatiesDto>()
                    .WhereEquals(x => x.Datum_boeking, bookingdate)
                    .Skip(-1) // for testing you can set skip = 0 and take = 5; the response will have 5 rows
                    .Take(-1) // 
                    .OrderBy(x => x.Administratie_code) //the following 4 OrderBy fields make the unique combination
                    .OrderBy(x => x.Journaalpost_nr)
                    .OrderBy(x => x.Journaalpost_vnr)
                    .OrderBy(x => x.Journaalpost_vnr_vb) //this field is not always used
                    .GetAsync();
                }

            }

           
            // financielemutatiesAC = await getConnector.clientAC.Query<DMFFinancieleMutatiesDto>()
            //.WhereEquals(x => x.Administratie_code, admin.Administratie_code.ToString())
            //.WhereEquals(x => x.Jaar, year.ToString())
            //.Skip(-1) //skip none
            //.Take(10) //take all
            //.OrderBy(x => x.Administratie_code)
            //.OrderBy(x => x.Journaalpost_nr)
            //.OrderBy(x => x.Journaalpost_vnr)
            //.OrderBy(x => x.Journaalpost_vnr_vb)
            //.GetAsync();

            // financielemutatiesAD = await getConnector.clientAD.Query<DMFFinancieleMutatiesDto>()
            //  .WhereEquals(x => x.Administratie_code, admin.Administratie_code.ToString())
            //  .WhereEquals(x => x.Jaar, year.ToString())
            //  .Skip(-1) //skip none
            //  .Take(10) //take all
            //  .OrderBy(x => x.Administratie_code)
            //  .OrderBy(x => x.Journaalpost_nr)
            //  .OrderBy(x => x.Journaalpost_vnr)
            //  .OrderBy(x => x.Journaalpost_vnr_vb)
            //  .GetAsync();

            // financielemutatiesAE = await getConnector.clientAE.Query<DMFFinancieleMutatiesDto>()
            // .WhereEquals(x => x.Administratie_code, admin.Administratie_code.ToString())
            // .WhereEquals(x => x.Jaar, year.ToString())
            // .Skip(-1) //skip none
            // .Take(10) //take all
            // .OrderBy(x => x.Administratie_code)
            // .OrderBy(x => x.Journaalpost_nr)
            // .OrderBy(x => x.Journaalpost_vnr)
            // .OrderBy(x => x.Journaalpost_vnr_vb)
            // .GetAsync();

            //       FormatDMFFinancieleMutatiesSummaryObject(financielemutatiesAA.ToList(), financielemutatiesAC.ToList(),
            //financielemutatiesAD.ToList(), financielemutatiesAE.ToList(), financielemutatiesDto);



        }

        public async Task<Result<PrometheusResponse>> DmfFinancieleMutatiesServiceAsync(DateTime startDate)
        {
            int startBusinessYear = startDate.Year;
            int currentYear = DateTime.UtcNow.Year;
            List<DMFFinancieleMutatiesDto> financielemutatiesDto = new List<DMFFinancieleMutatiesDto>();
            AfasCrenditalsDto getConnector = GetAfmaConnectors();

            var res = await _dmFFinancieleMutationService.GetInitialRecordAndLastRecordDatetime();
            var formatres = (MutationDto)res.Value.Data;


            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    //Load Initial Records  
                    if (formatres.IsInitialRecord == false)
                    {
                        var dto = await AdministrativeList();
                        FormatAdministrativeObject(dto._AA.ToList(), dto._AC.ToList(), dto._AD.ToList(), dto._AE.ToList(), administratiesdto);
                        foreach (var admin in administratiesdto)
                        {
                            for (int year = startBusinessYear; year < currentYear; year++)
                            {
                                await DmfFinancieleMutatiesServiceInitial(startBusinessYear, admin.Administratie_code, getConnector);
                                FormatDMFFinancieleMutatiesSummaryObject(financielemutatiesAA.ToList(), financielemutatiesDto);

                            }
                        }
                    }
                    //Get Changes Record  
                    else
                    {


                        //last mutation datetime (example 18-10-2022 12:30:05)

                        DateTime? dt = formatres.LastRecordDate;
                        string lastmutationdatetime = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dt); //check this date in the database 
                        string lastdayofthisyear = DateTime.Now.Year.ToString() + "-12-31T00:00:00.000"; //last day of current year

                        await DmfFinancieleMutatiesServiceExistingAsync(lastmutationdatetime, lastdayofthisyear, getConnector);
                        FormatDMFFinancieleMutatiesSummaryObject(financielemutatiesAA.ToList(), financielemutatiesDto);

                    } 

                    // DB operation
                    if (financielemutatiesDto.Count > 0)
                    {
                        var res = await _dmFFinancieleMutationService.BulkInsertWithProcAsync(financielemutatiesDto);
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
        private void FormatDMFFinancieleMutatiesSummaryObject(List<DMFFinancieleMutatiesDto> _financielemutatiesAA, List<DMFFinancieleMutatiesDto> _financielemutatiesDto)
        {
            _financielemutatiesAA.ForEach(a => a.Omgeving_code = "AA");
            _financielemutatiesDto.AddRange(_financielemutatiesAA);


        }
        private void FormatDMFFinancieleMutatiesSummaryObject(List<DMFFinancieleMutatiesDto> _financielemutatiesAA, List<DMFFinancieleMutatiesDto> _financielemutatiesAC,
                                                                List<DMFFinancieleMutatiesDto> _financielemutatiesAD, List<DMFFinancieleMutatiesDto> _financielemutatiesAE, List<DMFFinancieleMutatiesDto> _financielemutatiesDto)
        {
            _financielemutatiesAA.ForEach(a => a.Omgeving_code = "AA");
            _financielemutatiesDto.AddRange(_financielemutatiesAA);

            _financielemutatiesAC.ForEach(a => a.Omgeving_code = "AC");
            _financielemutatiesDto.AddRange(_financielemutatiesAC);

            _financielemutatiesAD.ForEach(a => a.Omgeving_code = "AD");
            _financielemutatiesDto.AddRange(_financielemutatiesAD);

            _financielemutatiesAE.ForEach(a => a.Omgeving_code = "AE");
            _financielemutatiesDto.AddRange(_financielemutatiesAE);
        }
        private void FormatAdministrativeObject(List<DMFAdministratiesDto> aa, List<DMFAdministratiesDto> ac, List<DMFAdministratiesDto> ad, List<DMFAdministratiesDto> ae, List<DMFAdministratiesDto> dto)
        {
            aa.ForEach(a => a.Omgeving_code = "AA");
            dto.AddRange(aa);

            ac.ForEach(a => a.Omgeving_code = "AC");
            dto.AddRange(ac);

            ad.ForEach(a => a.Omgeving_code = "AD");
            dto.AddRange(ad);

            ae.ForEach(a => a.Omgeving_code = "AE");
            dto.AddRange(ae);
        }

    }
}
