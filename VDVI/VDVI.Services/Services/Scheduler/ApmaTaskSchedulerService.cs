using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Microsoft.Extensions.Configuration;
using SOAPAppCore.Interfaces;
using System;
using System.Threading.Tasks;
using VDVI.DB.IRepository;
using VDVI.DB.Models.Common;
using VDVI.Services.Interfaces;
using VDVI.Services.Interfaces.Apma;

namespace VDVI.Services.Services
{
    public class ApmaTaskSchedulerService : IApmaTaskSchedulerService
    {
        public ITaskSchedulerRepository _taskScheduler;
        private readonly IHcsReportManagementSummaryService _reportSummary;
        private readonly IHcsBIReservationDashboardService _hcsBIReservationDashboardService;
        private readonly IHcsBIRatePlanStatisticsService _hcsBIRatePlanStatisticsService;
        private readonly IConfiguration _config;

        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();
        int actionflag = 0;
        public ApmaTaskSchedulerService(
            ITaskSchedulerRepository taskScheduler,
            IConfiguration config,
            IHcsReportManagementSummaryService reportSummary,
            IHcsBIReservationDashboardService hcsBIReservationDashboardService,
             IHcsBIRatePlanStatisticsService hcsBIRatePlanStatisticsService
            )
        {
            _taskScheduler = taskScheduler;
            _config = config;
            _reportSummary = reportSummary;
            _hcsBIReservationDashboardService = hcsBIReservationDashboardService;
            _hcsBIRatePlanStatisticsService = hcsBIRatePlanStatisticsService;
        }


        public async Task SummaryScheduler(string methodName)
        {
            bool flag = false;
            Result<PrometheusResponse> response;
            GetStartAndEndDate(methodName);

            switch (methodName)
            {
                case "HcsReportManagementSummary":
                    response = await _reportSummary.ReportManagementSummaryAsync(_startDate, _endDate);
                    flag = response.IsSuccess;
                    break;
                case "HcsBIReservationDashboard":
                    response = await _hcsBIReservationDashboardService.HcsBIReservationDashboardRepositoryAsyc(_startDate, _endDate);
                    flag = response.IsSuccess;
                    break;
                case "HcsBIRatePlanStatistics":
                    response = await _hcsBIRatePlanStatisticsService.HcsBIRatePlanStatisticsRepositoryAsyc(_startDate, _endDate);
                    flag = response.IsSuccess;
                    break;

                default:
                    break;
            }
            if (flag)
            _taskScheduler.InsertOrUpdateTaskScheduleDatetime(methodName, _endDate, actionflag);

        }


        private JobTaskScheduler GetStartAndEndDate(string methodName)
        {
            string resultDate = _config.GetSection("ApmaServiceDateConfig").GetSection("initialStartDate").Value;
            DateTime apmaInitialDate = Convert.ToDateTime(resultDate);

            var dayDiffernce = _config.GetSection("ApmaServiceDateConfig").GetSection("DayDifferenceReportManagementRoomAndLedgerSummary").Value;

            //Check from the Database by method Name, if there have any existing value or not ;
            JobTaskScheduler taskScheduleEndDate = _taskScheduler.GetTaskScheduler(methodName);

            if (taskScheduleEndDate == null)
            {
                actionflag = 0;
                _startDate = apmaInitialDate;
                _endDate = _startDate.AddDays(Convert.ToInt32(dayDiffernce));
            }
            else if (taskScheduleEndDate.LastExecutionDate != null)
            {
                _startDate = Convert.ToDateTime(taskScheduleEndDate.LastExecutionDate);
                _endDate = _startDate.AddDays(Convert.ToInt32(dayDiffernce));
                actionflag = 1;
            }
            return taskScheduleEndDate;
        }
    }
}
