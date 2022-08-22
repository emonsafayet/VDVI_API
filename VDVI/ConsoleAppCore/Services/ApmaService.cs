using Newtonsoft.Json;
using SOAPAppCore.Interfaces;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAppCore.Services
{
    public class ApmaService: IApmaService
    {

        SOAPService.HybridCloudEngineSoapClient client = new SOAPService.HybridCloudEngineSoapClient(SOAPService.HybridCloudEngineSoapClient.EndpointConfiguration.HybridCloudEngineSoap);

        AuthService authObj = new AuthService();

        public Task<HcsReportManagementSummaryResponse> HcsRptSummary(Authentication pmsAuthentication, string pmsProperty, DateTime StartDate, DateTime EndDate)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var reportManagementSummary = client.HcsReportManagementSummaryAsync(pmsAuthentication, PropertyCode: pmsProperty, StartDate: StartDate, EndDate: EndDate, "");

            //convert xml into json
            var trimDate = JsonConvert.SerializeObject(reportManagementSummary, formatting: Newtonsoft.Json.Formatting.Indented);
            return reportManagementSummary;
        }

        public string[] pmsGetProperties(Authentication pmsAuthentication)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var ListProperties = client.HcsListPropertiesAsync(pmsAuthentication, "", "").Result.HcsListPropertiesResult.Properties;

            List<string> propertylist= new List<string>();
            foreach (var item in ListProperties)
            {
                propertylist.Add(item.PropertyCode);
            }
            string[] properties = propertylist.ToArray();
            return properties;
        }

        public List<HcsReportManagementSummaryResponse> GetReportManagement(DateTime StartDate, DateTime EndDate)
        {
            var hcsReportManagementSummaryResponse = new List<HcsReportManagementSummaryResponse>();
            string pmsToken = authObj.AuthenticationResponse().Token;
            SOAPService.Authentication pmsAuthentication = authObj.Authentication(pmsToken);

            var properties = pmsGetProperties(pmsAuthentication);
            foreach (string pmsProperty in properties)
            {
                var res = HcsRptSummary(pmsAuthentication, pmsProperty, StartDate, EndDate).Result;
                hcsReportManagementSummaryResponse.Add(res);
            }
            return hcsReportManagementSummaryResponse;

        }
    }
}
