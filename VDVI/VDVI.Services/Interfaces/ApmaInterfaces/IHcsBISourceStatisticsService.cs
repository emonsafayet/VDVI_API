using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBISourceStatisticsService
    {
        Task<Result<PrometheusResponse>> HcsBIHcsBISourceStatisticsRepositoryHistoryAsyc(DateTime StartDate, DateTime EndDate); 
    }
}
