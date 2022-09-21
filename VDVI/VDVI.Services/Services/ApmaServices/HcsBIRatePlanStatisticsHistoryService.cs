﻿using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility; 
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Services.Interfaces; 

namespace VDVI.Services
{
    public class HcsBIRatePlanStatisticsHistoryService : ApmaBaseService, IHcsBIRatePlanStatisticsHistoryService
    {
        private readonly IHcsRatePlanStatisticsHistoryService _hcsRatePlanStatisticsService;
        public HcsBIRatePlanStatisticsHistoryService(IHcsRatePlanStatisticsHistoryService hcsRatePlanStatisticsService)
        {
            _hcsRatePlanStatisticsService = hcsRatePlanStatisticsService;
        }

        public async Task<Result<PrometheusResponse>> HcsBIRatePlanStatisticsRepositoryHistoryAsyc(DateTime StartDate, DateTime EndDate)
        {
            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                          async () =>
                          {
                              Authentication pmsAuthentication = GetApmaAuthCredential();
                              
                              List<RatePlanStatisticHistoryDto> dto = new List<RatePlanStatisticHistoryDto>();

                              foreach (string property in ApmaProperties)
                              {
                                  var res = await client.HcsBIRatePlanStatisticsAsync(pmsAuthentication, PropertyCode: property, StartDate: StartDate, EndDate: EndDate, "", "");

                                 var sourceStats = res.HcsBIRatePlanStatisticsResult.RatePlanStatistics.ToList();

                                  FormatSummaryObject(dto, sourceStats, property);
                              }

                              // DB operation
                              var dboccupanciesRes = _hcsRatePlanStatisticsService.BulkInsertWithProcAsync(dto);

                              return PrometheusResponse.Success("", "Data retrieval is successful");
                          },
                          exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                          {
                              DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                              RethrowException = false
                          });
        }

        private void FormatSummaryObject(List<RatePlanStatisticHistoryDto> ratePlanStatistics, List<BIRatePlanStatistic> dashboard, string propertyCode)
        {
            List<RatePlanStatisticHistoryDto> ratePlanStatistic = dashboard.Select(x => new RatePlanStatisticHistoryDto()
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
