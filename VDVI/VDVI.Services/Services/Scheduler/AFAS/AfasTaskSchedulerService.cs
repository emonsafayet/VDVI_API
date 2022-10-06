using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDVI.DB.Dtos;
using VDVI.Repository.Models.AfasModels.Dto;
using VDVI.Services.Interfaces;
using VDVI.Services.Interfaces.APMA; 

namespace VDVI.Services.APMA
{
    public class AfasTaskSchedulerService : IApmaTaskSchedulerService
    { 

        AfasSchedulerSetupDto dtos = new AfasSchedulerSetupDto();
        public AfasTaskSchedulerService( )
        {
            
        }
        public async Task SummaryScheduler()
        { }
    }
}
