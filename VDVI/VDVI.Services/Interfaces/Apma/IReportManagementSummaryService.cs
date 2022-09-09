using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using System;
using System.Threading.Tasks;
using VDVI.DB.Dtos;

namespace SOAPAppCore.Interfaces
{
    public interface IReportManagementSummaryService
    {
        Task<Result<PrometheusResponse>> ReportManagementSummaryAsync(DateTime StartDate, DateTime EndDate);
        Task<Result<PrometheusResponse>> InsertAsync(RoomSummaryDto dto);
    }
}
