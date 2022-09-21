using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Threading.Tasks;

namespace VDVI.Services.Interfaces
{
    public interface IHcsBIReservationDashboardFutureService
    {
        Task<Result<PrometheusResponse>> HcsBIReservationDashboardRepositoryAsyc(DateTime StartDate, DateTime EndDate);
    }
}
