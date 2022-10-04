using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Models.AfasModel;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Interfaces.AfasInterfaces.Administrators;
using VDVI.Services.Services.BaseService;

namespace VDVI.Services.AfasServices
{
    public class DmfAdministratiesService : AfasBaseService, IdmfAdministratiesService
    {
        public IdmfAdministraterService _dmfAdministraterService { get; }
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

                    var res = await clientAA.Query<DbDMFAdministraties>().Skip(-1).Take(-1).GetAsync();                    
                    var _ac = await clientAC.Query<DbDMFAdministraties>().Skip(-1).Take(-1).GetAsync();
                    var _ad = await clientAD.Query<DbDMFAdministraties>().Skip(-1).Take(-1).GetAsync();
                    var _ae = await clientAE.Query<DbDMFAdministraties>().Skip(-1).Take(-1).GetAsync();


                    // DB operation
                    //var res = _dmfAdministraterService.BulkInsertWithProcAsync(dto);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );
        }
    }
}
