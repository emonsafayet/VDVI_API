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
    public class DmfFinancieleMutatesService : AfasBaseService, IdmfFinancieleMutatiesService
    {
        private readonly IdmFFinancieleMutationService _dmFFinancieleMutationService;
        public DmfFinancieleMutatesService(IdmFFinancieleMutationService dmFFinancieleMutationService)
        {
            _dmFFinancieleMutationService = dmFFinancieleMutationService;
        }
        public async Task<Result<PrometheusResponse>> HcsDmfFinancieleMutatiesServiceAsyc(DateTime startDate)
        {
             return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                     

                    // DB operation
                     
                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );
        }

        private void FormatSummaryObject()
        {
          
        }

    }
}
