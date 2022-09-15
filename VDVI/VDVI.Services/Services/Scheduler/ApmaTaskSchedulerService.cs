using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Services.Interfaces;
using VDVI.Services;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using Ocelot.Responses;
using System.Threading;
using Unity;
using System.Collections.Generic;
using Quartz;

namespace VDVI.Services
{
    public class ApmaTaskSchedulerService : IApmaTaskSchedulerService
    {
        private readonly IHcsReportManagementSummaryService _reportSummary;
        private readonly IHcsBIReservationDashboardHistoryService _hcsBIReservationDashboardHistoryService;
        private readonly IHcsBIRatePlanStatisticsHistoryService _hcsBIRatePlanStatisticsHistoryService;
        private readonly IHcsBISourceStatisticsHistoryService _hcsBISourceStatisticsHistoryService;
        private readonly ISchedulerLogRepository _schedulerLogRepository;

        private readonly IHcsBISourceStatisticsFutureService _hcsBISourceStatisticsFutureService;
        private readonly ISchedulerSetupService _schedulerSetupService;

        private readonly IConfiguration _config;

        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();
        bool actionflag = false;

        SchedulerSetupDto dtos = new SchedulerSetupDto();
        public ApmaTaskSchedulerService(
            ISchedulerLogRepository schedulerLogRepository,
            IJobTaskSchedulerRepository jobTaskSchedulerRepository,
            IConfiguration config,
            IHcsReportManagementSummaryService reportSummary,
            IHcsBIReservationDashboardHistoryService hcsBIReservationDashboardHistoryService,
            IHcsBIRatePlanStatisticsHistoryService hcsBIRatePlanStatisticsHistoryService,
            IHcsBISourceStatisticsHistoryService hcsBISourceStatisticsHistoryService
            , IHcsBISourceStatisticsFutureService hcsBISourceStatisticsFutureService
           , ISchedulerSetupService schedulerSetupService

            )
        {
            _schedulerLogRepository = schedulerLogRepository;
            _config = config;
            _reportSummary = reportSummary;
            _hcsBIReservationDashboardHistoryService = hcsBIReservationDashboardHistoryService;
            _hcsBIRatePlanStatisticsHistoryService = hcsBIRatePlanStatisticsHistoryService;
            _hcsBISourceStatisticsHistoryService = hcsBISourceStatisticsHistoryService;
            _hcsBISourceStatisticsFutureService = hcsBISourceStatisticsFutureService;
            _schedulerSetupService = schedulerSetupService;
        }
        public async Task SummaryScheduler()
        {
            bool flag = false;
            Result<PrometheusResponse> response;
            DateTime forMateDateTime = DateTime.UtcNow;

            string resultDate = _config.GetSection("ApmaServiceDateConfig").GetSection("initialStartDate").Value;
            DateTime apmaInitialDate = Convert.ToDateTime(resultDate);

            var dayDifference = _config.GetSection("ApmaServiceDateConfig").GetSection("DayDifference").Value;
            DateTime currentDateTime = DateTime.UtcNow;

            var schedulers = await _schedulerSetupService.FindByAllScheduleAsync();

            foreach (var scheduler in schedulers)
            {
                if (currentDateTime > scheduler.NextExecutionDateTime || scheduler.NextExecutionDateTime == null)
                {
                    //History
                    if (!scheduler.isFuture && scheduler.NextExecutionDateTime == null)
                    {
                        _startDate = (DateTime)scheduler.BusinessStartDate;
                        _endDate = _startDate.AddDays(scheduler.DayDifference);
                    }

                    // for future Method
                    else if (scheduler.isFuture && scheduler.NextExecutionDateTime == null)
                        _startDate = new DateTime(forMateDateTime.Year, forMateDateTime.Month, forMateDateTime.Day, 0, 0, 0);

                    else if (scheduler.isFuture && scheduler.NextExecutionDateTime != null)
                        _startDate = (DateTime)scheduler.NextExecutionDateTime;

                    switch (scheduler.SchedulerName)
                    {
                        case "HcsReportManagementSummary":
                            response = await _reportSummary.ReportManagementSummaryAsync(_startDate, _endDate);
                            flag = response.IsSuccess;
                            break;
                        case "HcsBIRatePlanStatisticsHistory":
                            response = await _hcsBIRatePlanStatisticsHistoryService.HcsBIRatePlanStatisticsRepositoryHistoryAsyc(_startDate, _endDate);
                            flag = response.IsSuccess;
                            break;
                        case "HcsBIReservationDashboardHistory":
                            response = await _hcsBIReservationDashboardHistoryService.HcsBIReservationDashboardRepositoryAsyc(_startDate, _endDate);
                            flag = response.IsSuccess;
                            break;
                        case "HcsBISourceStatisticsHistory":
                            response = await _hcsBISourceStatisticsHistoryService.HcsBIHcsBISourceStatisticsRepositoryHistoryAsyc(_startDate, _endDate);
                            flag = response.IsSuccess;
                            break;

                        case "HcsBISourceStatisticsFuture":
                            response = await _hcsBISourceStatisticsFutureService.HcsBIHcsBISourceStatisticsRepositoryFutureAsyc(_startDate);
                            flag = response.IsSuccess;
                            break;

                        default:
                            break;
                    }

                }
                dtos.LastExecutionDateTime = _endDate;
                dtos.NextExecutionDateTime = _endDate.AddHours(dtos.NextExecutionHour); 
                dtos.SchedulerName = dtos.SchedulerName;
            }

            if (flag)
            {
                await _schedulerSetupService.UpdateAsync(dtos);
                //await _schedulerLogRepository.InsertAsync(logDtos);
            }
        }

    }
}
