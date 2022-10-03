using Microsoft.Extensions.Configuration;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Net;

namespace VDVI.Services
{
    public class ApmaBaseService
    {
        public HybridCloudEngineSoapClient client = new HybridCloudEngineSoapClient(HybridCloudEngineSoapClient.EndpointConfiguration.HybridCloudEngineSoap);
        public static string[] ApmaProperties;
        public static Authentication ApmaAuthCredential;

        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        AuthenticationResponse authenticationResponse = new AuthenticationResponse();   

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


            if (authenticationResponse.UTCExpirationDate < DateTime.UtcNow) // Token validation
                GetApmaApplicationResponse();

            var authCredential = new Authentication
            {
                User = _config.GetSection("ApmaAuthenticationCredential").GetSection("pmsUser").Value,
                Password = _config.GetSection("ApmaAuthenticationCredential").GetSection("pmsPassword").Value,
                VendorId = _config.GetSection("ApmaAuthenticationCredential").GetSection("pmsVendorId").Value,
                CrsProperty = _config.GetSection("ApmaAuthenticationCredential").GetSection("pmsCrsProperty").Value,
                Token = authenticationResponse.Token
            };


            // get ast properties
            ApmaProperties = GetApmaProperties(authCredential);

            // Set Auth
            ApmaAuthCredential = authCredential;

            return authCredential;
        }

        public AuthenticationResponse GetApmaApplicationResponse()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //request
            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                User = _config.GetSection("ApmaAuthenticationCredential").GetSection("pmsUser").Value,
                Password = _config.GetSection("ApmaAuthenticationCredential").GetSection("pmsPassword").Value,
                VendorId = _config.GetSection("ApmaAuthenticationCredential").GetSection("pmsVendorId").Value
            };

            authenticationResponse = client.HceAuthenticateAsync(authenticationRequest).Result; 
            
            return authenticationResponse;
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
