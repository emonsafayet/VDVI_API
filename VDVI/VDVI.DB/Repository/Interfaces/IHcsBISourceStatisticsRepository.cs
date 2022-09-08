﻿using System;
using System.Collections.Generic;
using System.Text;
using VDVI.DB.Models.ApmaModels;
using VDVI.Repository.Dtos.SourceStatistics;

namespace VDVI.DB.IRepository
{
    public  interface IHcsBISourceStatisticsRepository
    {
        string InsertHcsBISourceStatisticsHistory(List<SourceStatisticDto> sourceStatistics);
    }
}
