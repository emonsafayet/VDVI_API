using Microsoft.Extensions.Configuration;
using SOAPAppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VDVI.DB.IRepository;
using VDVI.DB.Models.ApmaModels;
using VDVI.DB.Repository;
using VDVI.Repository.Dtos.SourceStatistics;
using VDVI.Services.Interfaces; 

namespace VDVI.Services.Services
{
    public class HcsBISourceStatisticsService : IHcsBISourceStatisticsService
    {  
        private readonly IConfiguration _config; 
        private readonly IApmaTaskSchedulerService _apmaTaskSchedulerService;
        private readonly IHcsBISourceStatisticsRepository _hcsBISourceStatisticsRepository;
        List<SourceStatisticDto> list = new List<SourceStatisticDto>();

        public HcsBISourceStatisticsService(IConfiguration config,  IApmaTaskSchedulerService apmaTaskSchedulerService, 
            IHcsBISourceStatisticsRepository hcsBISourceStatisticsRepository)
        {
            _config = config; 
            _apmaTaskSchedulerService = apmaTaskSchedulerService;
            _hcsBISourceStatisticsRepository = hcsBISourceStatisticsRepository;
        }
        public void GetHcsBISourceStatistics()
        {
            
            _hcsBISourceStatisticsRepository.InsertHcsBISourceStatisticsHistory(list);
        }
    }
}
