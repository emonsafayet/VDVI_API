using Microsoft.Extensions.Configuration;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Net;

namespace VDVI.Services
{
    public class ApmaBaseService
    {
        public SOAPService.HybridCloudEngineSoapClient client = new SOAPService.HybridCloudEngineSoapClient(SOAPService.HybridCloudEngineSoapClient.EndpointConfiguration.HybridCloudEngineSoap);
        public static string ApmaAuthToken = null;
        public static string[] ApmaProperties;
        public static Authentication ApmaAuthCredential;


        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        // Duplicate here any configuration sources you use.        


        public IConfiguration _config;
        public ApmaBaseService()
        {


            configurationBuilder.AddJsonFile("AppSettings.json");
            _config = configurationBuilder.Build();
        }

        public Authentication GetApmaAuthCredential()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (ApmaAuthCredential != null)

                return ApmaAuthCredential;


            if (string.IsNullOrEmpty(ApmaAuthToken))
                GetApmaApplicationToken();

            var authCredential = new SOAPService.Authentication
            {
                User = _config.GetSection("AuthenticationCredential").GetSection("pmsUser").Value,
                Password = _config.GetSection("AuthenticationCredential").GetSection("pmsPassword").Value,
                VendorId = _config.GetSection("AuthenticationCredential").GetSection("pmsVendorId").Value,
                CrsProperty = _config.GetSection("AuthenticationCredential").GetSection("pmsCrsProperty").Value,
                Token = ApmaAuthToken
            };


            // get ast properties
            ApmaProperties = GetApmaProperties(authCredential);

            // Set Auth
            ApmaAuthCredential = authCredential;

            return authCredential;
        }

        public string GetApmaApplicationToken()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //request
            SOAPService.AuthenticationRequest authenticationRequest = new SOAPService.AuthenticationRequest
            {
                User = _config.GetSection("AuthenticationCredential").GetSection("pmsUser").Value,
                Password = _config.GetSection("AuthenticationCredential").GetSection("pmsPassword").Value,
                VendorId = _config.GetSection("AuthenticationCredential").GetSection("pmsVendorId").Value
            };

            SOAPService.AuthenticationResponse authenticationResponse = client.HceAuthenticateAsync(authenticationRequest).Result;

            ApmaAuthToken = authenticationResponse.Token;
            return authenticationResponse.Token;
        }

        public string[] GetApmaProperties(Authentication pmsAuthentication)
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
