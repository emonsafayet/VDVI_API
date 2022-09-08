//using CSharpFunctionalExtensions;
//using Framework.Core.Base.ModelEntity;
//using Framework.Core.Exceptions;
//using Framework.Core.Utility;
//using SOAPAppCore.Services;
//using SOAPService;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using VDVI.DB.IRepository;
//using VDVI.DB.Repository;
//using VDVI.Repository.Dtos.SourceStatistics;
//using VDVI.Services.Interfaces;

//namespace VDVI.Services.Services
//{
//    public class HcsBISourceStatisticsService : ApmaBaseService, IHcsBISourceStatisticsService
//    {
//        private readonly IHcsBISourceStatisticsRepository _hcsBISourceStatisticsRepository;
//        public HcsBISourceStatisticsService(HcsBISourceStatisticsRepository hcsBISourceStatisticsRepository)
//        {
//            _hcsBISourceStatisticsRepository = hcsBISourceStatisticsRepository;
//        }

//        public async Task<Result<PrometheusResponse>> HcsBIHcsBISourceStatisticsRepositoryAsyc(DateTime StartDate, DateTime EndDate)
//        {
//            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
//                          async () =>
//                          {
//                              Authentication pmsAuthentication = GetApmaAuthCredential();

//                              List<SourceStatisticDto> statisticDtos = new List<SourceStatisticDto>();

//                              foreach (var property in ApmaProperties)
//                              {
//                                  var res = await client.HcsBISourceStatisticsAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "", "");

//                                  var sourceStats = res.HcsBISourceStatisticsResult.SourceStatistics.ToList();

//                                  FormatSummaryObject(statisticDtos, sourceStats, property);
//                              }

//                              // DB operation
//                              var dbrevenuesRes = _hcsBISourceStatisticsRepository.InsertHcsBISourceStatisticsHistory(statisticDtos);

//                              return PrometheusResponse.Success("", "Data retrieval is successful");
//                          },
//                          exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
//                          {
//                              DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
//                              RethrowException = false
//                          });
//        }


//        private void FormatSummaryObject(List<SourceStatisticDto> sourceStatDtos, List<BISourceStatistic> sourceStats, string propertyCode)
//        {
//            List<SourceStatisticDto> sourceStatz = sourceStats.Select(x => new SourceStatisticDto()
//            {
//                BusinessDate = x.BusinessDate,
//                SourceCode = x.SourceCode,
//                NumberOfRooms = x.NumberOfRooms,
//                TotalRevenue = x.TotalRevenue,
//                TotalRevenueExcl = x.TotalRevenueExcl,
//                RevenueStatCodeA = x.RevenueStatCodeA,
//                RevenueStatCodeAExcl = x.RevenueStatCodeAExcl,
//                RevenueStatCodeB = x.RevenueStatCodeB,
//                RevenueStatCodeBExcl = x.RevenueStatCodeBExcl,
//                RevenueStatCodeC = x.RevenueStatCodeC,
//                RevenueStatCodeCExcl = x.RevenueStatCodeCExcl,
//                RevenueStatCodeD = x.RevenueStatCodeD,
//                RevenueStatCodeDExcl = x.RevenueStatCodeDExcl,
//                RevenueStatCodeE = x.RevenueStatCodeE,
//                RevenueStatCodeEExcl = x.RevenueStatCodeEExcl,
//                RevenueStatCodeF = x.RevenueStatCodeF,
//                RevenueStatCodeFExcl = x.RevenueStatCodeFExcl,
//                RevenueStatCodeUndefined = x.RevenueStatCodeUndefined,
//                RevenueStatCodeUndefinedExcl = x.RevenueStatCodeUndefinedExcl,
//                PropertyCode = propertyCode,
//            }).ToList();
//            sourceStatDtos.AddRange(sourceStatz);
//        }
//    }
//}
