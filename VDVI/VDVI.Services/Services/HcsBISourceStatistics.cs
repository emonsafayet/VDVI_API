using Microsoft.Extensions.Configuration;
using SOAPAppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VDVI.DB.IRepository;
using VDVI.DB.Repository;
using VDVI.Services.Interfaces;
using static VDVI.DB.Models.ApmaModels.HcsBISourceStatisticsModel;

namespace VDVI.Services.Services
{
    public class HcsBISourceStatistics : IHcsBISourceStatistics
    {  
        private readonly IConfiguration _config; 
        private readonly IApmaTaskSchedulerService _apmaTaskSchedulerService;
        public HcsBISourceStatistics(IConfiguration config,  IApmaTaskSchedulerService apmaTaskSchedulerService)
        {
            _config = config; 
            _apmaTaskSchedulerService = apmaTaskSchedulerService;
        }

        public void GetHcsBISourceStatistics()
        {
            var list = new List<SourceStatistic>();
        }
    }
}
