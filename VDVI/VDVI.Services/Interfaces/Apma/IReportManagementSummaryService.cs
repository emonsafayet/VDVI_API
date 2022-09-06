using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAppCore.Interfaces
{
    public interface IReportManagementSummaryService
    {
         Task<HcsReportManagementSummaryResponse> ReportManagementSummary(Authentication pmsAuthentication, string pmsProperty, DateTime StartDate, DateTime EndDate);
        

        List<HcsReportManagementSummaryResponse> GetReportManagementSummary(DateTime StartDate, DateTime EndDate);
        //Task<Result<PrometheusResponse>> ReportManagementSummaryAsync(string pmsProperty, DateTime StartDate, DateTime EndDate);
        //List<HcsReportManagementSummaryResponse> GetReportManagementSummary(DateTime StartDate, DateTime EndDate);
        //Task<HcsReportManagementSummaryResponse> ReportManagementSummary(Authentication pmsAuthentication, string pmsProperty, DateTime StartDate, DateTime EndDate);
    }
}
