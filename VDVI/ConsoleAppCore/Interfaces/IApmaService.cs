using SOAPService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAppCore.Interfaces
{
    public interface IApmaService
    {
         Task<HcsReportManagementSummaryResponse> HcsRptSummary(Authentication pmsAuthentication, string pmsProperty, DateTime StartDate, DateTime EndDate);
         public string[] pmsGetProperties(Authentication pmsAuthentication);

         public List<HcsReportManagementSummaryResponse> GetReportManagement(DateTime StartDate, DateTime EndDate);
    }
}
