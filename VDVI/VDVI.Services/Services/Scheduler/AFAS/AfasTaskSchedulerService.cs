using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using VDVI.Repository.Models.AfasModels.Dto;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Interfaces.AFAS;

namespace VDVI.Services.AFAS
{
    public class AfasTaskSchedulerService : IAfasTaskSchedulerService
    {
        private readonly IAfasSchedulerSetupService _afasschedulerSetupService;
        private readonly IAfasSchedulerLogService _afasSchedulerLogService;
        private readonly IAfasSchedulerSetupService _afasSchedulerSetupService;
        private readonly IdmfAdministratiesService _idmfAdministratiesService;
        private readonly IdmfBeginbalaniesService _idmfBeginbalaniesService;
        private readonly IdmfGrootboekrekeningen _idmfGrootboekrekeningen;

        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        public IConfiguration _config;

        AfasSchedulerSetupDto dtos = new AfasSchedulerSetupDto();
        public AfasTaskSchedulerService(IAfasSchedulerSetupService afasschedulerSetupService,
            IdmfAdministratiesService idmfAdministratiesService,
            IdmfBeginbalaniesService idmfBeginbalaniesService,
            IAfasSchedulerLogService afasSchedulerLogService,
            IAfasSchedulerSetupService afasSchedulerSetupService,
            IdmfGrootboekrekeningen idmfGrootboekrekeningen)
        {
            _afasschedulerSetupService = afasschedulerSetupService;
            _idmfAdministratiesService = idmfAdministratiesService;
            _afasSchedulerLogService = afasSchedulerLogService;
            _afasSchedulerSetupService = afasSchedulerSetupService;
            _idmfBeginbalaniesService = idmfBeginbalaniesService;
            _idmfGrootboekrekeningen = idmfGrootboekrekeningen;

            configurationBuilder.AddJsonFile("AppSettings.json");
            _config = configurationBuilder.Build();
        }


        public async Task SummaryScheduler()
        {
            bool flag = false;
            Result<PrometheusResponse> response;
            DateTime currentDateTime = DateTime.UtcNow;
            var logDayLimits = Convert.ToInt32(_config.GetSection("SchedulerLog").GetSection("AFASSchedulerLogLimitDays").Value);

            var afasschedulers = await _afasschedulerSetupService.FindByAllScheduleAsync();
            var new1 = afasschedulers.ToList();

            for (int i = 0; i < new1.Count(); i++)
            {
                var afasscheduler = new1[i];
                if (afasscheduler.NextExecutionDateTime <= currentDateTime || afasscheduler.NextExecutionDateTime == null)
                {
                    switch (afasscheduler.SchedulerName)
                    {
                        case "DMFAdministraties":
                            response = await _idmfAdministratiesService.HcsDmfAdministratiesAsyc();
                            flag = response.IsSuccess;
                            break; 
                        case "DMFBeginbalans":
                            response = await _idmfBeginbalaniesService.HcsDmfBeginbalaniesServiceAsyc((DateTime)afasscheduler.BusinessStartDate);
                            flag = response.IsSuccess;
                            break;
                        case "DMFGrootboekrekeningen":
                            response = await _idmfGrootboekrekeningen.HcsDmfGrootboekrekeningensServiceAsyc();
                            flag = response.IsSuccess;
                            break;
                        default:
                            break;
                    }                

                    var timeOfDay = currentDateTime.AddMinutes(afasscheduler.ExecutionIntervalMins).TimeOfDay;  
                    var nextFullHour = TimeSpan.FromHours(Math.Ceiling(timeOfDay.TotalHours)); // Get next execution hours from full dateTime and celling into next hour.

                    if ((timeOfDay.Hours) == 0)
                        currentDateTime = currentDateTime.AddDays(1);  // case : 1=> it will be work last hour on the dayend ; case 2(worst case)=>  it will be execute after at 11.59 PM on last day of the year | Like (year :2022,month :11,day: 31,hour : 11,min:56,sec : 10) - this will be 2023/01/01: 01:00:00; 
                    var dateFormat = currentDateTime.ToString("MM/dd/yyyy");

                    dtos.LastExecutionDateTime = currentDateTime;
                    dtos.NextExecutionDateTime = Convert.ToDateTime(dateFormat) + nextFullHour;
                    dtos.SchedulerName = afasscheduler.SchedulerName;

                    if (flag)
                    {
                        await _afasSchedulerSetupService.SaveWithProcAsync(dtos);
                        await _afasSchedulerLogService.SaveWithProcAsync(afasscheduler.SchedulerName, logDayLimits);
                    }
                }
            }
        }
    }
}
