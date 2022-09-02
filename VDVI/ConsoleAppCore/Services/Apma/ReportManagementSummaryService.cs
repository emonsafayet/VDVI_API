using Microsoft.Extensions.Configuration;
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

        public IConfiguration _config;

        public ReportManagementSummaryService(IConfiguration config)
        {
            _config = config;
        }
        public Task<HcsReportManagementSummaryResponse> ReportManagementSummary(Authentication pmsAuthentication, string pmsProperty, DateTime StartDate, DateTime EndDate)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                var reportManagementSummary = client.HcsReportManagementSummaryAsync(pmsAuthentication, PropertyCode: pmsProperty, StartDate: StartDate, EndDate: EndDate, "");

                //convert xml into json
                var trimDate = JsonConvert.SerializeObject(reportManagementSummary, formatting: Formatting.Indented);
                return reportManagementSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string[] ReportManagementSummaryGetProperties(Authentication pmsAuthentication)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                var ListProperties = client.HcsListPropertiesAsync(pmsAuthentication, "", "").Result.HcsListPropertiesResult.Properties;

                List<string> propertylist = new List<string>();
                foreach (var item in ListProperties)
                {
                    propertylist.Add(item.PropertyCode);
                }
                string[] properties = propertylist.ToArray();
                return properties;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<HcsReportManagementSummaryResponse> GetReportManagementSummary(DateTime StartDate, DateTime EndDate)
        {
            var hcsReportManagementSummaryResponse = new List<HcsReportManagementSummaryResponse>();
            var existingToken = _config.GetSection("AuthenticationToken").GetSection("Token").Value;

            string pmsToken = existingToken;
            Authentication pmsAuthentication = authObj.Authentication(pmsToken);

            try
            {
                var properties = ReportManagementSummaryGetProperties(pmsAuthentication);

                // if token is invalid
                if (properties.Length <= 0 || existingToken == null)
                {
                    pmsToken = authObj.AuthenticationResponse().Token;
                    pmsAuthentication = authObj.Authentication(pmsToken);
                    properties = ReportManagementSummaryGetProperties(pmsAuthentication);
                    _config.GetSection("AuthenticationToken").GetSection("Token").Value = pmsToken;
                }

                if (properties.Length > 0)
                {
                    foreach (string pmsProperty in properties)
                    {
                        var res = ReportManagementSummary(pmsAuthentication, pmsProperty, StartDate, EndDate).Result;
                        hcsReportManagementSummaryResponse.Add(res);
                    }
                }

                return hcsReportManagementSummaryResponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
