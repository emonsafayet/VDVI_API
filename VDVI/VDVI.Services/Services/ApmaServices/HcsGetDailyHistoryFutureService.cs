using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using VDVI.DB.Dtos; 
using VDVI.Services.Interfaces;

namespace VDVI.Services
{
    public class HcsGetDailyHistoryFutureService : ApmaBaseService, IHcsGetDailyFutureService
    {
        private readonly IHcsDailyFutureService _hcsDailyFutureService;

        public HcsGetDailyHistoryFutureService(IHcsDailyFutureService hcsDailyFutureService)
        {
            _hcsDailyFutureService = hcsDailyFutureService;
        }
        public async Task<Result<PrometheusResponse>> HcsGetDailyHistoryFutureAsyc(DateTime lastExecutionDate, int dayDifference)
        {
            DateTime nextExecutionDate = lastExecutionDate.AddMonths(12).AddSeconds(1);
            DateTime tempDate = lastExecutionDate;

            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                 async () =>
                 {
                     Authentication pmsAuthentication = GetApmaAuthCredential();

                     List<DailyHistoryFutureDto> dto = new List<DailyHistoryFutureDto>();
                     HcsGetDailyHistoryResponse res = new HcsGetDailyHistoryResponse();
                     while (tempDate < nextExecutionDate)
                     {
                         var endDate = tempDate.AddDays(dayDifference);
                         endDate = endDate > nextExecutionDate ? nextExecutionDate : endDate;

                         for (int i = 0; i < ApmaProperties.Length; i++)
                         {
                             var propertyCode = ApmaProperties[i];
                             int x = 0;
                             res = await client.HcsGetDailyHistoryAsync(pmsAuthentication, PropertyCode: propertyCode, StartDate: tempDate, EndDate: endDate, "", 0, 30, "");
                             do
                             {
                                 var dailyHistoryList = res.HcsGetDailyHistoryResult.DailyHistories.ToList();
                                 FormatSummaryObject(dto, dailyHistoryList, propertyCode);
                                 x++;
                             } while (x < res.HcsGetDailyHistoryResult.TotalPages);
                         }
                         tempDate = tempDate.AddDays(dayDifference).AddSeconds(1);
                     }

                     var result = _hcsDailyFutureService.BulkInsertWithProcAsync(dto);

                     return PrometheusResponse.Success("", "Data retrieval is successful");
                 },
                 exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                 {
                     DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                     RethrowException = false
                 });
        }
        private void FormatSummaryObject(List<DailyHistoryFutureDto> sourceStatDtos, List<DailyHistory> dailyHistoryList, string propertyCode)
        {
            List<DailyHistoryFutureDto> sourceStatz = dailyHistoryList.Select(x => new DailyHistoryFutureDto()
            {
                PropertyCode = propertyCode,
                Date = x.Date,
                PmsSegmentNumber = x.PmsSegmentNumber,
                PmsSegmentType = x.PmsSegmentType,
                RoomType = x.RoomType,
                Source = x.Source,
                SubSource = x.SubSource,
                RateType = x.RateType,
                Mealplan = x.Mealplan,
                Package = x.Package,
                CountryIso2Code = x.CountryIso2Code,
                PaymentDebitor = x.PaymentDebitor,
                PaymentNonDebitor = x.PaymentNonDebitor,
                Adults = x.Adults,
                Children = x.Children,
                Infants = x.Infants,
                IsDayuse = x.IsDayuse,

                RevenueInclusiveA = x.RevenuesInclusive.RevenueA,
                RevenueInclusiveB = x.RevenuesInclusive.RevenueB,
                RevenueInclusiveC = x.RevenuesInclusive.RevenueC,
                RevenueInclusiveD = x.RevenuesInclusive.RevenueD,
                RevenueInclusiveE = x.RevenuesInclusive.RevenueE,
                RevenueInclusiveF = x.RevenuesInclusive.RevenueF,
                RevenueInclusiveUndefined = x.RevenuesInclusive.RevenueUndefined,

                RevenueExclusiveA = x.RevenuesExclusive.RevenueA,
                RevenueExclusiveB = x.RevenuesExclusive.RevenueB,
                RevenueExclusiveC = x.RevenuesExclusive.RevenueC,
                RevenueExclusiveD = x.RevenuesExclusive.RevenueD,
                RevenueExclusiveE = x.RevenuesExclusive.RevenueE,
                RevenueExclusiveF = x.RevenuesExclusive.RevenueF,
                RevenueExclusiveUndefined = x.RevenuesExclusive.RevenueUndefined

            }).ToList();
            sourceStatDtos.AddRange(sourceStatz);
        }

    }
}
