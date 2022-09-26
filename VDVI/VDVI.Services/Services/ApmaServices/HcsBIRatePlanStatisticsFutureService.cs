using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Services.Interfaces;

namespace VDVI.Services.Services.ApmaServices
{
    public class HcsBIRatePlanStatisticsFutureService : ApmaBaseService, IHcsBIRatePlanStatisticsFutureService
    {
        private readonly IHcsRatePlanStatisticsFutureService _hcsRatePlanStatisticsService;
        public HcsBIRatePlanStatisticsFutureService(IHcsRatePlanStatisticsFutureService hcsRatePlanStatisticsService)
        {
            _hcsRatePlanStatisticsService = hcsRatePlanStatisticsService;
        }

        public async Task<Result<PrometheusResponse>> HcsBIRatePlanStatisticsRepositoryFutureAsyc(DateTime lastExecutionDate, int dayDifference)
        {
            DateTime nextExecutionDate = lastExecutionDate.AddMonths(12).AddSeconds(1);
            DateTime tempDate = lastExecutionDate;

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    Authentication pmsAuthentication = GetApmaAuthCredential();

                    List<RatePlanStatisticFutureDto> dto = new List<RatePlanStatisticFutureDto>();
                    while (tempDate < nextExecutionDate)
                    {
                        var endDate = tempDate.AddDays(dayDifference);
                        endDate = endDate > nextExecutionDate ? nextExecutionDate : endDate;

                        for (int i = 0; i < ApmaProperties.Length; i++)
                        {
                            var propertyCode = ApmaProperties[i];
                            var res = await client.HcsBIRatePlanStatisticsAsync(pmsAuthentication, PropertyCode: propertyCode, StartDate: tempDate, EndDate: endDate, "","");

                            var sourceStats = res.HcsBIRatePlanStatisticsResult.RatePlanStatistics.ToList();

                            FormatSummaryObject(dto, sourceStats, propertyCode);
                        }
                        tempDate = tempDate.AddDays(dayDifference).AddSeconds(1);
                    } 

                    // DB operation
                    var dboccupanciesRes = _hcsRatePlanStatisticsService.BulkInsertWithProcAsync(dto);

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );
        }

        private void FormatSummaryObject(List<RatePlanStatisticFutureDto> ratePlanStatistics, List<BIRatePlanStatistic> dashboard, string propertyCode)
        {
            List<RatePlanStatisticFutureDto> ratePlanStatistic = dashboard.Select(x => new RatePlanStatisticFutureDto()
            {
                PropertyCode = propertyCode,
                BusinessDate = x.BusinessDate,
                RatePlan = x.RatePlan,
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
                RevenueStatCodeUndefinedExcl = x.RevenueStatCodeUndefinedExcl
            }).ToList();

            ratePlanStatistics.AddRange(ratePlanStatistic);
        }
    }
}
