using Microsoft.Extensions.Configuration;
using SOAPAppCore.Interfaces;
using System;
using System.Threading.Tasks;
using VDVI.DB.IRepository;
using VDVI.DB.Models.Common;
using VDVI.Services.Interfaces;

namespace VDVI.Services.Services
{
    public class ApmaTaskSchedulerService : IApmaTaskSchedulerService
    {
        public ITaskSchedulerRepository _taskScheduler;
        private readonly IReportManagementSummaryService _reportSummary;
        private readonly IConfiguration _config;

        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();

        public ApmaTaskSchedulerService(
            ITaskSchedulerRepository taskScheduler,
            IConfiguration config,
            IReportManagementSummaryService reportSummary
            )
        {
            _taskScheduler = taskScheduler;
            _config = config;
            _reportSummary = reportSummary;
        }


        public async Task SummaryScheduler()
        {
            GetStartAndEndDate();

            var methodName = "HcsReportManagementSummary";
            int flag = 1;

            var response = await _reportSummary.ReportManagementSummaryAsync(_startDate, _endDate);

            if (response.IsSuccess)
                _taskScheduler.InsertOrUpdateTaskScheduleDatetime(methodName, _endDate, flag);

        }


        private JobTaskScheduler GetStartAndEndDate()
        {
            string resultDate = _config.GetSection("ApmaServiceDateConfig").GetSection("initialStartDate").Value;
            DateTime apmaInitialDate = Convert.ToDateTime(resultDate);

            var dayDiffernce = _config.GetSection("ApmaServiceDateConfig").GetSection("DayDifferenceReportManagementRoomAndLedgerSummary").Value;

            //Check from the Database by method Name, if there have any existing value or not ;
            JobTaskScheduler taskScheduleEndDate = _taskScheduler.GetTaskScheduler("HcsReportManagementSummary");

            if (taskScheduleEndDate == null)
            {
                _startDate = apmaInitialDate;
                _endDate = _startDate.AddDays(Convert.ToInt32(dayDiffernce));
            }
            else if (taskScheduleEndDate.LastExecutionDate != null)
            {
                _startDate = Convert.ToDateTime(taskScheduleEndDate.LastExecutionDate);
                _endDate = _startDate.AddDays(Convert.ToInt32(dayDiffernce));
            }
            return taskScheduleEndDate;
        }
    }
}
