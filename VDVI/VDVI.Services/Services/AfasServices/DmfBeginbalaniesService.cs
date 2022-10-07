using CSharpFunctionalExtensions; 
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Interfaces.AfasInterfaces.Administrators;
using VDVI.Services.Services.BaseService;

namespace VDVI.Services.AfasServices
{
    public class DmfBeginbalaniesService : AfasBaseService, IdmfBeginbalaniesService
    {
        private readonly IdmfBeginbalansService _dmfBeginbalansService;
        public DmfBeginbalaniesService(IdmfBeginbalansService dmfBeginbalansService)
        {
            _dmfBeginbalansService = dmfBeginbalansService;
        }
        public async Task<Result<PrometheusResponse>> HcsDmfBeginbalaniesServiceAsyc(DateTime startDate)
        {
            int startBusinessYear= startDate.Year;
            int currentYear = DateTime.UtcNow.Year;
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    List<DMFBeginbalansDto> dto = new List<DMFBeginbalansDto>();
                    var getConnector = GetAfmaConnectors();
                    do
                    {
                        //Netherlands (=Dutch)=aa  | Spain =ac| Bonaire =ad | Belgium=ae
                        var _aa = await getConnector.clientAA.Query<DMFBeginbalansDto>().WhereEquals(x => x.Jaar, startBusinessYear.ToString()).Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();
                        var _ac = await getConnector.clientAC.Query<DMFBeginbalansDto>().WhereEquals(x => x.Jaar, startBusinessYear.ToString()).Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();
                        var _ad = await getConnector.clientAD.Query<DMFBeginbalansDto>().WhereEquals(x => x.Jaar, startBusinessYear.ToString()).Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();
                        var _ae = await getConnector.clientAE.Query<DMFBeginbalansDto>().WhereEquals(x => x.Jaar, startBusinessYear.ToString()).Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();

                        //Format data
                        FormatSummaryObject(_aa.ToList(), _ac.ToList(), _ad.ToList(), _ae.ToList(), dto);
                        startBusinessYear++;
                    } while (startBusinessYear<= currentYear);
                  

                    // DB operation
                    var res = _dmfBeginbalansService.BulkInsertWithProcAsync(dto);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );
        }

        private void FormatSummaryObject(List<DMFBeginbalansDto> aa, List<DMFBeginbalansDto> ac, List<DMFBeginbalansDto> ad, List<DMFBeginbalansDto> ae, List<DMFBeginbalansDto> dto)
        {
            List<DMFBeginbalansDto> aaList = aa.Select(x => new DMFBeginbalansDto()
            {
                Omgeving_code = "AA",
                Administratie_code = x.Administratie_code,
                Rekeningnummer = x.Rekeningnummer,
                Jaar = x.Jaar,
                Periode = x.Periode,
                Datum_boeking = x.Datum_boeking,
                Saldo = x.Saldo,

            }).ToList();
            dto.AddRange(aaList); 

            List<DMFBeginbalansDto> acList = ac.Select(x => new DMFBeginbalansDto()
            {
                Omgeving_code = "AC",
                Administratie_code = x.Administratie_code,
                Rekeningnummer = x.Rekeningnummer,
                Jaar = x.Jaar,
                Periode = x.Periode,
                Datum_boeking = x.Datum_boeking,
                Saldo = x.Saldo,

            }).ToList();
            dto.AddRange(acList);

            List<DMFBeginbalansDto> adList = ad.Select(x => new DMFBeginbalansDto()
            {
                Omgeving_code = "AD",
                Administratie_code = x.Administratie_code,
                Rekeningnummer = x.Rekeningnummer,
                Jaar = x.Jaar,
                Periode = x.Periode,
                Datum_boeking = x.Datum_boeking,
                Saldo = x.Saldo,

            }).ToList();
            dto.AddRange(adList);

            List<DMFBeginbalansDto> aeList = ae.Select(x => new DMFBeginbalansDto()
            {
                Omgeving_code = "AE",
                Administratie_code = x.Administratie_code,
                Rekeningnummer = x.Rekeningnummer,
                Jaar = x.Jaar,
                Periode = x.Periode,
                Datum_boeking = x.Datum_boeking,
                Saldo = x.Saldo,

            }).ToList();
            dto.AddRange(aeList);
        }

    }
}
