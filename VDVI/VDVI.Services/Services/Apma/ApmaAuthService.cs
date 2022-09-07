using Microsoft.Extensions.Configuration;
using SOAPAppCore.Interfaces;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAppCore.Services
{
    public class ApmaAuthService: IApmaAuthService
    {
       SOAPService.HybridCloudEngineSoapClient client = new SOAPService.HybridCloudEngineSoapClient(SOAPService.HybridCloudEngineSoapClient.EndpointConfiguration.HybridCloudEngineSoap);

        public IConfiguration _config;
        public ApmaAuthService(IConfiguration config)
        {
            _config = config;
        }  

        public Authentication Authentication(string pmsToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

           var authentication = new SOAPService.Authentication
           {
                User = _config.GetSection("AuthenticationCredential").GetSection("pmsUser").Value,
                Password = _config.GetSection("AuthenticationCredential").GetSection("pmsPassword").Value,
                VendorId = _config.GetSection("AuthenticationCredential").GetSection("pmsVendorId").Value,
                CrsProperty = _config.GetSection("AuthenticationCredential").GetSection("pmsCrsProperty").Value,
                Token = pmsToken
            };
            return authentication;
        }
        public AuthenticationResponse AuthenticationResponse()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //request
            SOAPService.AuthenticationRequest authenticationRequest = new SOAPService.AuthenticationRequest
            {
                User = _config.GetSection("AuthenticationCredential").GetSection("pmsUser").Value,
                Password = _config.GetSection("AuthenticationCredential").GetSection("pmsPassword").Value,
                VendorId = _config.GetSection("AuthenticationCredential").GetSection("pmsVendorId").Value
            };

            //response
            SOAPService.AuthenticationResponse authenticationResponse = client.HceAuthenticateAsync(authenticationRequest).Result;
            return authenticationResponse;
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
    }
}
