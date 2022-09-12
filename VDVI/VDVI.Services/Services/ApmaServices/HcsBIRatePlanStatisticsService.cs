using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SOAPAppCore.Services;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.RoomSummary;
using VDVI.Repository.Interfaces;
using VDVI.Services.Interfaces.Apma;

namespace VDVI.Services.Services.Apma
{
    public class HcsBIRatePlanStatisticsService : ApmaBaseService, IHcsBIRatePlanStatisticsService
    {
        private readonly IHcsBIRatePlanStatisticsRepository _hcsBIRatePlanStatisticsRepository;
        public HcsBIRatePlanStatisticsService(IHcsBIRatePlanStatisticsRepository hcsBIRatePlanStatisticsRepository)
        {
            _hcsBIRatePlanStatisticsRepository = hcsBIRatePlanStatisticsRepository;
        }

        public async Task<Result<PrometheusResponse>> HcsBIRatePlanStatisticsRepositoryAsyc(DateTime StartDate, DateTime EndDate)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                          async () =>
                          {
                              Authentication pmsAuthentication = GetApmaAuthCredential();

                              List<DbRatePlanStatistic> ratePlanStatistics = new List<DbRatePlanStatistic>();

                              foreach (string property in ApmaProperties)
                              {
                                  var res = await client.HcsBIRatePlanStatisticsAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "", "");

                                  List<BIRatePlanStatistic> bIRatePlanStatistic = res.HcsBIRatePlanStatisticsResult.RatePlanStatistics.ToList();

                                  FormatSummaryObject(ratePlanStatistics, bIRatePlanStatistic, property);
                              }

                              // DB operation
                              var dboccupanciesRes = _hcsBIRatePlanStatisticsRepository.InsertRatePlanStatisticHistory(ratePlanStatistics);

                              return PrometheusResponse.Success("", "Data retrieval is successful");
                          },
                          exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                          {
                              DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                              RethrowException = false
                          });
        }

        private void FormatSummaryObject(List<DbRatePlanStatistic> ratePlanStatistics, List<BIRatePlanStatistic> dashboard, string propertyCode)
        {
            List<DbRatePlanStatistic> ratePlanStatistic = dashboard.Select(x => new DbRatePlanStatistic()
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
