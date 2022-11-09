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
        List<DMFBoekingsdagenMutatiesDto> Dto = new List<DMFBoekingsdagenMutatiesDto>();
        private DateTime currentDate = DateTime.Now;

        public DmfBoekingsdagenMutatesService(IdmFBoekingsdagenMutationService dmFBoekingsdagenMutationService)
        {
            _dmFBoekingsdagenMutationService = dmFBoekingsdagenMutationService;
        }

        public async Task<Result<PrometheusResponse>> DmfBoekingsdagenMutatiesServiceAsync()
        {
            AfasCrenditalsDto getConnector = GetAfmaConnectors();
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {

                    await LoadAsync(getConnector);
                   
                    // DB operation
                    if (Dto.Count > 0)
                    {
                        await _dmFBoekingsdagenMutationService.BulkInsertWithProcAsync(Dto, true);
                        Dto.Clear();
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
        public async Task LoadAsync(AfasCrenditalsDto getConnector)
        {
            AA = await getConnector.clientAA.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).GetAsync();
            if (AA.Length > 0) FormatSummaryObjectAA(AA.ToList(), Dto);
            AC = await getConnector.clientAC.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).GetAsync();
            if (AC.Length > 0) FormatSummaryObjectAC(AC.ToList(), Dto);
            AD = await getConnector.clientAD.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).GetAsync();
            if (AD.Length > 0) FormatSummaryObjectAD(AD.ToList(), Dto);
            AE = await getConnector.clientAE.Query<DMFBoekingsdagenMutatiesDto>().Skip(-1).Take(-1).GetAsync();
            if (AE.Length > 0) FormatSummaryObjectAE(AE.ToList(), Dto);

            
        }

        private void FormatSummaryObjectAA(List<DMFBoekingsdagenMutatiesDto> _AA, List<DMFBoekingsdagenMutatiesDto> _Dto)
        {
            if (_AA.Count > 0)
            { 
                _AA.ForEach(a => a.Omgeving_code = "AA");
                _Dto.AddRange(_AA);
            }
        }
        private void FormatSummaryObjectAC(List<DMFBoekingsdagenMutatiesDto> _AC, List<DMFBoekingsdagenMutatiesDto> _Dto)
        {
            if (_AC.Count > 0)
            {
                _AC.ForEach(a => a.Omgeving_code = "AC");
                _Dto.AddRange(_AC);
            }
        }
        private void FormatSummaryObjectAD(List<DMFBoekingsdagenMutatiesDto> _AD, List<DMFBoekingsdagenMutatiesDto> _Dto)
        {
            if (_AD.Count > 0)
            {
                _AD.ForEach(a => a.Omgeving_code = "AD");
                _Dto.AddRange(_AD);
            }
        }
        private void FormatSummaryObjectAE(List<DMFBoekingsdagenMutatiesDto> _AE, List<DMFBoekingsdagenMutatiesDto> _Dto)
        {
            if (_AE.Count > 0)
            {
                _AE.ForEach(a => a.Omgeving_code = "AE");
                _Dto.AddRange(_AE);
            }
        }
    }
}
