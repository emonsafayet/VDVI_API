using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Microsoft.Extensions.Configuration; 
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Dtos.SourceStatistics;
using VDVI.Services.Interfaces; 

namespace VDVI.Services
{
    public class HcsBISourceStatisticsFutureService : ApmaBaseService, IHcsBISourceStatisticsFutureService
    { 
        private readonly IHcsSourceStasticsFutureService _hcsSourceStasticsFutureService;

        public HcsBISourceStatisticsFutureService(IHcsSourceStasticsFutureService hcsSourceStasticsFutureService)
        {
            _hcsSourceStasticsFutureService = hcsSourceStasticsFutureService;
        } 
       public async Task<Result<PrometheusResponse>> HcsBIHcsBISourceStatisticsRepositoryFutureAsyc(DateTime StartDate, DateTime EndDate)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                 async () =>
                 {
                     Authentication pmsAuthentication = GetApmaAuthCredential();

                     List<SourceStatisticFutureDto> dto = new List<SourceStatisticFutureDto>();

                     foreach (string property in ApmaProperties)
                     {
                         var res = await client.HcsBISourceStatisticsAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "", "");

                         var sourceStats = res.HcsBISourceStatisticsResult.SourceStatistics.ToList();

                         FormatSummaryObject(dto, sourceStats, property);
                     }

                     //This dto list need to implement
                     var dbrevenuesRes = _hcsSourceStasticsFutureService.BulkInsertAsync(dto);

                     return PrometheusResponse.Success("", "Data retrieval is successful");
                 },
                 exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                 {
                     DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                     RethrowException = false
                 });
        }
       private void FormatSummaryObject(List<SourceStatisticFutureDto> sourceStatDtos, List<BISourceStatistic> sourceStats, string propertyCode)
        {
            List<SourceStatisticFutureDto> sourceStatz = sourceStats.Select(x => new SourceStatisticFutureDto()
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
