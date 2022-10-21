using CSharpFunctionalExtensions; 
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Nelibur.ObjectMapper;
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
    public class DmfAdministratiesService : AfasBaseService, IdmfAdministratiesService
    {
        private readonly IdmfAdministraterService _dmfAdministraterService;
        List<DMFAdministratiesDto>  administratiesdto = new List<DMFAdministratiesDto>();
        public DmfAdministratiesService(IdmfAdministraterService dmfAdministraterService)
        {
            _dmfAdministraterService = dmfAdministraterService;
        } 

        public async Task<Result<PrometheusResponse>> DmfAdministratiesAsync()
        {

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    var dto = await AdministrativeList();
                    FormatSummaryObject(dto._AA.ToList(), dto._AC.ToList(), dto._AD.ToList(), dto._AE.ToList(), administratiesdto);

                    // DB operation
                     var res = _dmfAdministraterService.BulkInsertWithProcAsync(administratiesdto);

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
