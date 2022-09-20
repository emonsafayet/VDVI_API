using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBISourceStatisticsFutureService
    {
       Task<Result<PrometheusResponse>> HcsBIHcsBISourceStatisticsRepositoryFutureAsyc(DateTime lastExecutionDate,int dayDifference);
    }
}
