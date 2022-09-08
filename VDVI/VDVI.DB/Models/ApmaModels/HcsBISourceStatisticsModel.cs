using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.DB.Models.ApmaModels
{
   
        public class HcsBISourceStatisticsResult
        {
            public HcsBISourceStatistics hcsBISourceStatistics;
        }
        public class HcsBISourceStatistics
        {
            public string PropertyCode { get; set; }
            public SourceStatisticDto SourceStatistics { get; set; }
            public string UniqueID { get; set; }
            public bool Success { get; set; }
            public object ErrorInfo { get; set; }
            public object WarningInfo { get; set; }
        }

        
}
