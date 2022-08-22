using SOAPAppCore.Interfaces;
using SOAPAppCore.Interfaces.Apma;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOAPAppCore.Services.Apma
{
    public class ReportManagementSummary: IReportManagementSummary
    {
        private readonly IApmaService _apmaService;

        public ReportManagementSummary(IApmaService apmaService)
        {
           _apmaService = apmaService;
        }
        public  List<HcsReportManagementSummaryResponse> GetReportManagementSummaryFromApma(DateTime startDate, DateTime endDate)
        {
            List<HcsReportManagementSummaryResponse> res = new List<HcsReportManagementSummaryResponse>();
            try
            {
                //DateTime StartDate = Convert.ToDateTime(startDate.ToString());
                //DateTime Enddate = Convert.ToDateTime(endDate.ToString());
                res = _apmaService.GetReportManagement(startDate, endDate);

                Console.WriteLine(res);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return res;
        }

    }
}
