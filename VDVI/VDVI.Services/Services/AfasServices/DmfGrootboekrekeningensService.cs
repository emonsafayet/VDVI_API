using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;
using VDVI.Repository.AfasModels;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Services.BaseService;

namespace VDVI.Services.AfasServices
{
    public class DmfGrootboekrekeningensService : AfasBaseService, IdmfGrootboekrekeningen
    {
        private readonly IdmfGrootboekrekeningenService _dmfGrootboekrekeningenService;
        public DmfGrootboekrekeningensService(IdmfGrootboekrekeningenService dmfGrootboekrekeningenService)
        {
            _dmfGrootboekrekeningenService = dmfGrootboekrekeningenService;
        }
        public async Task<Result<PrometheusResponse>> DmfFinancieleMutatiesServiceAsync()
        { 
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    List<DMFGrootboekrekeningenDto> dto = new List<DMFGrootboekrekeningenDto>();
                    var getConnector = GetAfmaConnectors();

                    //Netherlands (=Dutch)=aa  | Spain =ac| Bonaire =ad | Belgium=ae
                    var _aa = await getConnector.clientAA.Query<DMFGrootboekrekeningenDto>().Skip(-1).Take(-1).OrderBy(x => x.Rekeningnummer).GetAsync();
                    var _ac = await getConnector.clientAC.Query<DMFGrootboekrekeningenDto>().Skip(-1).Take(-1).OrderBy(x => x.Rekeningnummer).GetAsync();
                    var _ad = await getConnector.clientAD.Query<DMFGrootboekrekeningenDto>().Skip(-1).Take(-1).OrderBy(x => x.Rekeningnummer).GetAsync();
                    var _ae = await getConnector.clientAE.Query<DMFGrootboekrekeningenDto>().Skip(-1).Take(-1).OrderBy(x => x.Rekeningnummer).GetAsync();

                    //Format data
                    FormatSummaryObject(_aa.ToList(), _ac.ToList(), _ad.ToList(), _ae.ToList(), dto);


                    // DB operation
                    var res = _dmfGrootboekrekeningenService.BulkInsertWithProcAsync(dto);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );
        }

        private void FormatSummaryObject(List<DMFGrootboekrekeningenDto> aa, List<DMFGrootboekrekeningenDto> ac, List<DMFGrootboekrekeningenDto> ad, List<DMFGrootboekrekeningenDto> ae, List<DMFGrootboekrekeningenDto> dto)
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
