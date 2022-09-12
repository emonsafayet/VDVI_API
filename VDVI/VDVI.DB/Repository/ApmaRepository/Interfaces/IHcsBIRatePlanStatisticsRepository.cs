using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Dtos.Accounts;

namespace VDVI.Repository.Repository.Interfaces
{
    public interface IHcsBIRatePlanStatisticsRepository
    {
        string InsertRatePlanStatisticHistory(List<DbRatePlanStatistic> ratePlanStatisticDto);
        
    }
}
