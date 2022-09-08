using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Threading.Tasks;

namespace SOAPAppCore.Interfaces
{
    public interface IHcsReportManagementSummaryService
    {
        Task<Result<PrometheusResponse>> ReportManagementSummaryAsync(DateTime StartDate, DateTime EndDate);
    }
}
