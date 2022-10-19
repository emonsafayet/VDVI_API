using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Nelibur.ObjectMapper;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;
using VDVI.Repository.AfasModels;
using VDVI.Repository.Dtos.AfasDtos.Administrations;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Interfaces.AfasInterfaces.Administrators;
using VDVI.Services.Services.BaseService;

namespace VDVI.Services.AfasServices
{
    public class DmfFinancieleMutatesService : AfasBaseService, IdmfFinancieleMutatiesService
    {
        private readonly IdmFFinancieleMutationService _dmFFinancieleMutationService;
        List<DMFAdministratiesDto> administratiesdto = new List<DMFAdministratiesDto>();
        public DmfFinancieleMutatesService(IdmFFinancieleMutationService dmFFinancieleMutationService)
        {
            _dmFFinancieleMutationService = dmFFinancieleMutationService;
        }
        public async Task<Result<PrometheusResponse>> HcsDmfFinancieleMutatiesServiceAsyc(DateTime startDate)
        {
            int startBusinessYear = startDate.Year;
            int currentYear = DateTime.UtcNow.Year;

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var financielemutatiesDto = new List<DMFFinancieleMutatiesDto>();
                    var dto = await AdministrativeList();
                    var getConnector = GetAfmaConnectors();
                    FormatAdministrativeObject(dto._AA.ToList(), dto._AC.ToList(), dto._AD.ToList(), dto._AE.ToList(), administratiesdto);

                    foreach (var admin in administratiesdto)
                    {
                        for (int year = startBusinessYear; year < currentYear; year++)
                        {
                            var financielemutatiesAA = await getConnector.clientAA.Query<DMFFinancieleMutatiesDto>()
                            .WhereEquals(x => x.Administratie_code, admin.Administratie_code.ToString()).
                            WhereEquals(x => x.Jaar, year.ToString())
                           .Skip(-1) //skip none
                           .Take(-1) //take all
                           .OrderBy(x => x.Administratie_code)  
                           .OrderBy(x => x.Journaalpost_nr)
                           .OrderBy(x => x.Journaalpost_vnr)
                           .OrderBy(x => x.Journaalpost_vnr_vb)  
                           .GetAsync();

                            var financielemutatiesAC = await getConnector.clientAC.Query<DMFFinancieleMutatiesDto>()
                            .WhereEquals(x => x.Administratie_code, admin.Administratie_code.ToString()).
                            WhereEquals(x => x.Jaar, year.ToString())
                           .Skip(-1) //skip none
                           .Take(-1) //take all
                           .OrderBy(x => x.Administratie_code)  
                           .OrderBy(x => x.Journaalpost_nr)
                           .OrderBy(x => x.Journaalpost_vnr)
                           .OrderBy(x => x.Journaalpost_vnr_vb) 
                           .GetAsync();

                            var financielemutatiesAD = await getConnector.clientAD.Query<DMFFinancieleMutatiesDto>()
                            .WhereEquals(x => x.Administratie_code, admin.Administratie_code.ToString()).
                            WhereEquals(x => x.Jaar, year.ToString())
                           .Skip(-1) //skip none
                           .Take(-1) //take all
                           .OrderBy(x => x.Administratie_code) 
                           .OrderBy(x => x.Journaalpost_nr)
                           .OrderBy(x => x.Journaalpost_vnr)
                           .OrderBy(x => x.Journaalpost_vnr_vb) 
                           .GetAsync();

                            var financielemutatiesAE = await getConnector.clientAE.Query<DMFFinancieleMutatiesDto>()
                          .WhereEquals(x => x.Administratie_code, admin.Administratie_code.ToString())
                         .WhereEquals(x => x.Jaar, year.ToString())
                          .Skip(-1) //skip none
                          .Take(-1) //take all
                          .OrderBy(x => x.Administratie_code)  
                          .OrderBy(x => x.Journaalpost_nr)
                          .OrderBy(x => x.Journaalpost_vnr)
                          .OrderBy(x => x.Journaalpost_vnr_vb) 
                          .GetAsync();

                           FormatDMFFinancieleMutatiesSummaryObject(financielemutatiesAA.ToList(), financielemutatiesAC.ToList(),
                                                                financielemutatiesAD.ToList(), financielemutatiesAE.ToList(),
                                                                    financielemutatiesDto);
                        }
                    }

                    // DB operation
                    var res = _dmFFinancieleMutationService.BulkInsertWithProcAsync(financielemutatiesDto);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );
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
