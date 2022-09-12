using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Microsoft.Extensions.Configuration;
using SOAPAppCore.Services;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Dtos.SourceStatistics;
using VDVI.Services.Interfaces;
using VDVI.Services.Interfaces.ApmaInterfaces;
using VDVI.Services.Services.ApmaServices.SourceStatistics.History;

namespace VDVI.Services.Services
{
    public class HcsBISourceStatisticsService : ApmaBaseService, IHcsBISourceStatisticsService
    {
        private readonly IHcsSourceStasticsHistoryService _hcsSourceStasticsHistoryService;

        public HcsBISourceStatisticsService(IHcsSourceStasticsHistoryService hcsSourceStasticsHistoryService)
        {
            _hcsSourceStasticsHistoryService = hcsSourceStasticsHistoryService;
        } 
       
        public async Task<Result<PrometheusResponse>> HcsBIHcsBISourceStatisticsRepositoryAsyc(DateTime StartDate, DateTime EndDate)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                 async () =>
                 {
                     Authentication pmsAuthentication = GetApmaAuthCredential();

                     List<SourceStatisticHistoryDto> dto = new List<SourceStatisticHistoryDto>(); 

                     foreach (string property in ApmaProperties)
                     {
                         var res = await client.HcsBISourceStatisticsAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "", "");

                         var sourceStats = res.HcsBISourceStatisticsResult.SourceStatistics.ToList();

                         FormatSummaryObject(dto, sourceStats, property);
                     }


                     var dbrevenuesRes = _hcsSourceStasticsHistoryService.BulkInsertAsync(dto);

                     return PrometheusResponse.Success("", "Data retrieval is successful");
                 },
                 exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                 {
                     DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                     RethrowException = false
                 });
        }
        private void FormatSummaryObject(List<SourceStatisticHistoryDto> sourceStatDtos, List<BISourceStatistic> sourceStats, string propertyCode)
        {
            List<SourceStatisticHistoryDto> sourceStatz = sourceStats.Select(x => new SourceStatisticHistoryDto()
            {
                BusinessDate = x.BusinessDate,
                SourceCode = x.SourceCode,
                NumberOfRooms = x.NumberOfRooms,
                TotalRevenue = x.TotalRevenue,
                TotalRevenueExcl = x.TotalRevenueExcl,
                RevenueStatCodeA = x.RevenueStatCodeA,
                RevenueStatCodeAExcl = x.RevenueStatCodeAExcl,
                RevenueStatCodeB = x.RevenueStatCodeB,
                RevenueStatCodeBExcl = x.RevenueStatCodeBExcl,
                RevenueStatCodeC = x.RevenueStatCodeC,
                RevenueStatCodeCExcl = x.RevenueStatCodeCExcl,
                RevenueStatCodeD = x.RevenueStatCodeD,
                RevenueStatCodeDExcl = x.RevenueStatCodeDExcl,
                RevenueStatCodeE = x.RevenueStatCodeE,
                RevenueStatCodeEExcl = x.RevenueStatCodeEExcl,
                RevenueStatCodeF = x.RevenueStatCodeF,
                RevenueStatCodeFExcl = x.RevenueStatCodeFExcl,
                RevenueStatCodeUndefined = x.RevenueStatCodeUndefined,
                RevenueStatCodeUndefinedExcl = x.RevenueStatCodeUndefinedExcl,
                PropertyCode = propertyCode,
            }).ToList();
            sourceStatDtos.AddRange(sourceStatz);
        }
    }
} 
