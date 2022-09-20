using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBIRatePlanStatisticsFutureService
    {
        Task<Result<PrometheusResponse>> HcsBIRatePlanStatisticsRepositoryFutureAsyc(DateTime StartDate, DateTime EndDate);
    }
}
