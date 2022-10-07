using CSharpFunctionalExtensions;
using DutchGrit.Afas;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos;
using VDVI.Repository.Models.AfasModel;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Interfaces.AfasInterfaces.Administrators;
using VDVI.Services.Services.BaseService;

namespace VDVI.Services.AfasServices
{
    public class DmfAdministratiesService : AfasBaseService, IdmfAdministratiesService
    {
        private readonly IdmfAdministraterService _dmfAdministraterService;
        public DmfAdministratiesService(IdmfAdministraterService dmfAdministraterService)
        {
            _dmfAdministraterService = dmfAdministraterService;
        }
        public async Task<Result<PrometheusResponse>> HcsDmfAdministratiesAsyc()
        {

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    List<DMFAdministratiesDto> dto = new List<DMFAdministratiesDto>();
                    var getConnector = GetAfmaConnectors();

                    var _aa = await getConnector.clientAA.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).GetAsync();
                    var _ac = await getConnector.clientAC.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).GetAsync();
                    var _ad = await getConnector.clientAD.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).GetAsync();
                    var _ae = await getConnector.clientAE.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).GetAsync();

                    //Format data
                    FormatSummaryObject(_aa.ToList(), _ac.ToList(), _ad.ToList(), _ae.ToList(), dto);

                    // DB operation
                    var res = _dmfAdministraterService.BulkInsertWithProcAsync(dto);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );
        }

        private void FormatSummaryObject(List<DMFAdministratiesDto> aa, List<DMFAdministratiesDto> ac, List<DMFAdministratiesDto> ad, List<DMFAdministratiesDto> ae, List<DMFAdministratiesDto> dto)
        {
            List<DMFAdministratiesDto> aaList = aa.Select(x => new DMFAdministratiesDto()
            {
                Omgeving_code = "AA",
                Administratietype_code = x.Administratietype_code,
                Administratie=x.Administratie,
                Administratie_code=x.Administratie_code,    
                Administratietype=x.Administratietype

            }).ToList();
            dto.AddRange(aaList); 

            List<DMFAdministratiesDto> acList = ac.Select(x => new DMFAdministratiesDto()
            {
                Omgeving_code = "AC",
                Administratietype_code = x.Administratietype_code,
                Administratie=x.Administratie,
                Administratie_code=x.Administratie_code,    
                Administratietype=x.Administratietype

            }).ToList();
            dto.AddRange(acList);

            List<DMFAdministratiesDto> adList = ad.Select(x => new DMFAdministratiesDto()
            {
                Omgeving_code = "AD",
                Administratietype_code = x.Administratietype_code,
                Administratie = x.Administratie,
                Administratie_code = x.Administratie_code,
                Administratietype = x.Administratietype

            }).ToList();
            dto.AddRange(adList);

            List<DMFAdministratiesDto> aeList = ae.Select(x => new DMFAdministratiesDto()
            {
                Omgeving_code = "AE",
                Administratietype_code = x.Administratietype_code,
                Administratie = x.Administratie,
                Administratie_code = x.Administratie_code,
                Administratietype = x.Administratietype

            }).ToList();
            dto.AddRange(aeList);
        }

    }
}
