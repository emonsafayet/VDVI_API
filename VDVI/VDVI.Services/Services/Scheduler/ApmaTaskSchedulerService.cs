using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Services.Interfaces;

namespace VDVI.Services
{
    public class ApmaTaskSchedulerService : IApmaTaskSchedulerService
    {
        private readonly IHcsReportManagementSummaryService _reportSummary;
        private readonly IJobTaskSchedulerRepository _jobTaskSchedulerRepository;

        //private readonly IHcsBIReservationDashboardService _hcsBIReservationDashboardService;
        //private readonly IHcsBIRatePlanStatisticsService _hcsBIRatePlanStatisticsService;
        //private readonly IHcsBISourceStatisticsService _hcsBISourceStatisticsService;
        private readonly IConfiguration _config;

        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();
        int actionflag = 0;
        public ApmaTaskSchedulerService(
            IJobTaskSchedulerRepository jobTaskSchedulerRepository,
            IConfiguration config,
            IHcsReportManagementSummaryService reportSummary
            //IHcsBIReservationDashboardService hcsBIReservationDashboardService,
            //IHcsBIRatePlanStatisticsService hcsBIRatePlanStatisticsService
            //IHcsBISourceStatisticsService hcsBISourceStatisticsService
            )
        {
            _jobTaskSchedulerRepository = jobTaskSchedulerRepository;
            _config = config;
            _reportSummary = reportSummary;
            //_hcsBIReservationDashboardService = hcsBIReservationDashboardService;
            //_hcsBIRatePlanStatisticsService = hcsBIRatePlanStatisticsService;
            // _hcsBISourceStatisticsService = hcsBISourceStatisticsService;
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
                //case "HcsBIReservationDashboard":
                //    response = await _hcsBIReservationDashboardService.HcsBIReservationDashboardRepositoryAsyc(_startDate, _endDate);
                //    flag = response.IsSuccess;
                //    break;
                //case "HcsBIRatePlanStatistics":
                //    response = await _hcsBIRatePlanStatisticsService.HcsBIRatePlanStatisticsRepositoryAsyc(_startDate, _endDate);
                //    flag = response.IsSuccess;
                //    break;
                //case "HcsBISourceStatisticsService":
                //    response = await _hcsBISourceStatisticsService.HcsBIHcsBISourceStatisticsRepositoryAsyc(_startDate, _endDate);
                //    flag = response.IsSuccess;
                //    break;

                default:
                    break;
            }
            JobTaskSchedulerDto dto = new JobTaskSchedulerDto()
            {
                LastExecutionDate = _endDate,
                flag = flag,
                MethodName = methodName
            };
            if (flag)
               await _jobTaskSchedulerRepository.SaveWithProcAsync(dto);

        }


        private async Task<string> GetStartAndEndDate(string methodName)
        {
            string resultDate = _config.GetSection("ApmaServiceDateConfig").GetSection("initialStartDate").Value;
            DateTime apmaInitialDate = Convert.ToDateTime(resultDate);

            var dayDiffernce = _config.GetSection("ApmaServiceDateConfig").GetSection("DayDifferenceReportManagementRoomAndLedgerSummary").Value;

            //Check from the Database by method Name, if there have any existing value or not ;
            var taskScheduleEndDate =await _jobTaskSchedulerRepository.FindByMethodNameAsync(methodName);

            if (taskScheduleEndDate == null)
            {
                actionflag = 0;
                _startDate = apmaInitialDate;
                _endDate = _startDate.AddDays(Convert.ToInt32(dayDiffernce));
            }
            else if (taskScheduleEndDate != null)
            {
                _startDate = Convert.ToDateTime(taskScheduleEndDate);
                _endDate = _startDate.AddDays(Convert.ToInt32(dayDiffernce));
                actionflag = 1;
            }
            return taskScheduleEndDate;
        }
    }
}
