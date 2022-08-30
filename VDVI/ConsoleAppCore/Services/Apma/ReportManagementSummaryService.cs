using Newtonsoft.Json;
using SOAPAppCore.Interfaces;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAppCore.Services.Apma
{
    public class ReportManagementSummaryService : IReportManagementSummaryService
    {

        HybridCloudEngineSoapClient client = new HybridCloudEngineSoapClient(HybridCloudEngineSoapClient.EndpointConfiguration.HybridCloudEngineSoap);

        ApmaAuthService authObj = new ApmaAuthService();

        public Task<HcsReportManagementSummaryResponse> ReportManagementSummary(Authentication pmsAuthentication, string pmsProperty, DateTime StartDate, DateTime EndDate)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var reportManagementSummary = client.HcsReportManagementSummaryAsync(pmsAuthentication, PropertyCode: pmsProperty, StartDate: StartDate, EndDate: EndDate, "");

            //convert xml into json
            var trimDate = JsonConvert.SerializeObject(reportManagementSummary, formatting: Formatting.Indented);
            return reportManagementSummary;
        }

        public string[] ReportManagementSummaryGetProperties(Authentication pmsAuthentication)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var ListProperties = client.HcsListPropertiesAsync(pmsAuthentication, "", "").Result.HcsListPropertiesResult.Properties;

            List<string> propertylist = new List<string>();
            foreach (var item in ListProperties)
            {
                propertylist.Add(item.PropertyCode);
            }
            string[] properties = propertylist.ToArray();
            return properties;
        }

        public List<HcsReportManagementSummaryResponse> GetReportManagementSummary(DateTime StartDate, DateTime EndDate)
        {
            var hcsReportManagementSummaryResponse = new List<HcsReportManagementSummaryResponse>();
            string pmsToken = authObj.AuthenticationResponse().Token;
            Authentication pmsAuthentication = authObj.Authentication(pmsToken);

            var properties = ReportManagementSummaryGetProperties(pmsAuthentication);
            foreach (string pmsProperty in properties)
            {
                var res = ReportManagementSummary(pmsAuthentication, pmsProperty, StartDate, EndDate).Result;
                hcsReportManagementSummaryResponse.Add(res);
            }
            return hcsReportManagementSummaryResponse;

        }
    }
}
