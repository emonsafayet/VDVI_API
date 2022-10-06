using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Models.AfasModels.Dto;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Interfaces;
using VDVI.Services.Interfaces.AFAS;
using VDVI.Services.Interfaces.APMA;

namespace VDVI.Services.AFAS
{
    public class AfasTaskSchedulerService : IAfasTaskSchedulerService
    {
        private readonly IAfasSchedulerSetupService _afasschedulerSetupService;
        private readonly IdmfAdministratiesService _idmfAdministratiesService;
        private readonly IAfasSchedulerLogService _afasSchedulerLogService;
        private readonly IAfasSchedulerSetupService _afasSchedulerSetupService;

        AfasSchedulerSetupDto dtos = new AfasSchedulerSetupDto();
        public AfasTaskSchedulerService(IAfasSchedulerSetupService afasschedulerSetupService,
            IdmfAdministratiesService idmfAdministratiesService,
            IAfasSchedulerLogService afasSchedulerLogService,
            IAfasSchedulerSetupService afasSchedulerSetupService)
        {
            _afasschedulerSetupService = afasschedulerSetupService;
            _idmfAdministratiesService = idmfAdministratiesService;
            _afasSchedulerLogService = afasSchedulerLogService;
            _afasSchedulerSetupService = afasSchedulerSetupService;
        }


        public async Task SummaryScheduler()
        {
            bool flag = false;
            Result<PrometheusResponse> response;
            DateTime currentDateTime = DateTime.UtcNow;

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
                        default:
                            break;
                    }
                    dtos.LastExecutionDateTime = DateTime.UtcNow;
                    dtos.NextExecutionDateTime = DateTime.UtcNow.AddMinutes(afasscheduler.ExecutionIntervalMins);
                    dtos.SchedulerName = afasscheduler.SchedulerName;

                    if (flag)
                    {
                        await _afasSchedulerSetupService.SaveWithProcAsync(dtos);
                        await _afasSchedulerLogService.SaveWithProcAsync(afasscheduler.SchedulerName);
                    }
                }
            }
        }
    }
}
