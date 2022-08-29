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
         public string[] ReportManagementSummaryGetProperties(Authentication pmsAuthentication);

         public List<HcsReportManagementSummaryResponse> GetReportManagementSummary(DateTime StartDate, DateTime EndDate);
    }
}
