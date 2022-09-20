﻿using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using VDVI.Services.Interfaces;

namespace VDVI.Services
{
    public class ApmaTaskSchedulerService : IApmaTaskSchedulerService
    {
        private readonly IHcsReportManagementSummaryService _reportSummary;
        private readonly IHcsBIReservationDashboardHistoryService _hcsBIReservationDashboardHistoryService;
        private readonly IHcsBIRatePlanStatisticsHistoryService _hcsBIRatePlanStatisticsHistoryService;
        private readonly IHcsBISourceStatisticsHistoryService _hcsBISourceStatisticsHistoryService;

        private readonly IHcsBISourceStatisticsFutureService _hcsBISourceStatisticsFutureService;
        private readonly ISchedulerSetupService _schedulerSetupService;
        public readonly ISchedulerLogService _schedulerLogService;
        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();

        SchedulerSetupDto dtos = new SchedulerSetupDto();
        public ApmaTaskSchedulerService(
            IHcsReportManagementSummaryService reportSummary,
            IHcsBIReservationDashboardHistoryService hcsBIReservationDashboardHistoryService,
            IHcsBIRatePlanStatisticsHistoryService hcsBIRatePlanStatisticsHistoryService,
            IHcsBISourceStatisticsHistoryService hcsBISourceStatisticsHistoryService
            , IHcsBISourceStatisticsFutureService hcsBISourceStatisticsFutureService
           , ISchedulerSetupService schedulerSetupService
           , ISchedulerLogService schedulerLogService

            )
        {
            _reportSummary = reportSummary;
            _hcsBIReservationDashboardHistoryService = hcsBIReservationDashboardHistoryService;
            _hcsBIRatePlanStatisticsHistoryService = hcsBIRatePlanStatisticsHistoryService;
            _hcsBISourceStatisticsHistoryService = hcsBISourceStatisticsHistoryService;
            _hcsBISourceStatisticsFutureService = hcsBISourceStatisticsFutureService;
            _schedulerSetupService = schedulerSetupService;
            _schedulerLogService = schedulerLogService;
        }
        public async Task SummaryScheduler()
        {
            bool flag = false;
            Result<PrometheusResponse> response; 
            DateTime currentDateTime = DateTime.UtcNow;

            var schedulers = await _schedulerSetupService.FindByAllScheduleAsync();
            var new1 = schedulers.ToList();

            for (int i = 0; i < new1.Count(); i++)
            {
                var scheduler = new1[i];


                if (currentDateTime > scheduler.NextExecutionDateTime || scheduler.NextExecutionDateTime == null)
                {
                   
                    //History
                    if (scheduler.isFuture==false 
                        && scheduler.NextExecutionDateTime == null) 
                    {
                        _startDate = (DateTime)scheduler.BusinessStartDate;
                        _endDate = _startDate.AddDays(scheduler.DayDifference);
                    }
                    else if (scheduler.isFuture==false
                        && scheduler.NextExecutionDateTime != null) 
                    {
                        _startDate = (DateTime)scheduler.NextExecutionDateTime;
                        _endDate = _startDate.AddDays(scheduler.DayDifference);
                    }

                    // for future Method
                    else if (scheduler.isFuture && scheduler.NextExecutionDateTime == null)
                        _startDate = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 0, 0, 0);

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

                    dtos.LastExecutionDateTime = scheduler.isFuture==false? _endDate:_startDate;
                    dtos.NextExecutionDateTime = scheduler.isFuture == false?
                                                _endDate.AddHours(scheduler.NextExecutionHour):
                                                _startDate.AddHours(scheduler.NextExecutionHour);
                    dtos.SchedulerName = scheduler.SchedulerName;

                    if (flag)
                    {
                        await _schedulerSetupService.SaveWithProcAsync(dtos);                    
                        await _schedulerLogService.SaveWithProcAsync(scheduler.SchedulerName);
                    }
                                           
                }
               
            }            
        } 
    }
}
