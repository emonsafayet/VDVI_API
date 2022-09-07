using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;

namespace VDVI.DB.IRepository
{
    public  interface IHcsBISourceStatisticsRepository
    {
        string InsertHcsBISourceStatisticsHistory(List<SourceStatistic> sourceStatistics);
    }
}
