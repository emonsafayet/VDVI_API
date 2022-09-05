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

        //ApmaAuthService authObj = new ApmaAuthService();

        public IConfiguration _config;
        private IApmaAuthService _apmaAuthService;

        public ReportManagementSummaryService(IConfiguration config, IApmaAuthService apmaAuthService)
        {
            _config = config;
            _apmaAuthService = apmaAuthService;
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

        public List<HcsReportManagementSummaryResponse> GetReportManagementSummary(DateTime StartDate, DateTime EndDate)
        {
            var hcsReportManagementSummaryResponse = new List<HcsReportManagementSummaryResponse>();
            var existingToken = _config.GetSection("AuthenticationCredential").GetSection("Token").Value;

            string pmsToken = existingToken;
            Authentication pmsAuthentication = _apmaAuthService.Authentication(pmsToken);

            try
            {
                var properties = _apmaAuthService.ReportManagementSummaryGetProperties(pmsAuthentication);

                // if token is invalid
                if (properties.Length <= 0 || existingToken == null)
                {
                    pmsToken = _apmaAuthService.AuthenticationResponse().Token;
                    pmsAuthentication = _apmaAuthService.Authentication(pmsToken);
                    properties = _apmaAuthService.ReportManagementSummaryGetProperties(pmsAuthentication);
                    _config.GetSection("AuthenticationCredential").GetSection("Token").Value = pmsToken;
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
