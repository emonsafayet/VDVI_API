using SOAPService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOAPAppCore.Interfaces.Apma
{
    public interface IReportManagementSummary
    {
        public List<HcsReportManagementSummaryResponse> GetReportManagementSummaryFromApma(DateTime startDate, DateTime endDate);
    }
}
