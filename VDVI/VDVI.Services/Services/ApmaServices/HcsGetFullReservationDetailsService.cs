using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility; 
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Services.Interfaces; 

namespace VDVI.Services
{
    public class HcsGetFullReservationDetailsService : ApmaBaseService, IHcsGetFullReservationDetailsService
    {

        private readonly IHcsGetFullReservationDetailService _hcsGetFullReservationDetailService;
        List<GetFullReservationDetailsDto> ReservationDetailsdto = new List<GetFullReservationDetailsDto>();
        public HcsGetFullReservationDetailsService(
            IHcsGetFullReservationDetailService hcsGetFullReservationDetailService)
        {
            _hcsGetFullReservationDetailService = hcsGetFullReservationDetailService; 
        }

        public async Task<Result<PrometheusResponse>> HcsGetFullReservationDetailsAsync()
        {

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    Authentication pmsAuthentication = GetApmaAuthCredential();

                    GetFullReservationDetailsDto dto = new GetFullReservationDetailsDto(); 

                    foreach (string property in ApmaProperties)
                    {
                        var res = await  client.HcsGetFullReservationDetailsAsync(pmsAuthentication, PropertyCode: property,"", "TIE-FX127097", "","","");                         
                        //FormatSummaryObject(dto, property);
                    } 
                    var result = _hcsGetFullReservationDetailService.BulkInsertWithProcAsync(ReservationDetailsdto); 

                    return PrometheusResponse.Success("", result.ToString());
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                });
        }

        private void FormatSummaryObject(GetFullReservationDetailsDto dto, string propertyCode)
        {
            //dto.PropertyCode = propertyCode;
            //ReservationDetailsdto.AddRange(dto); 
        }
    }
}
