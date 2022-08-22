using SOAPAppCore.Interfaces;
using SOAPService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAppCore.Services
{
    public class AuthService: IAuthService
    {
       SOAPService.HybridCloudEngineSoapClient client = new SOAPService.HybridCloudEngineSoapClient(SOAPService.HybridCloudEngineSoapClient.EndpointConfiguration.HybridCloudEngineSoap);

        string pmsUser = "Valk_ETL_22";
        string pmsPassword = "O86n9JPr8jFG";
        string pmsVendorId = "0013200001Dj3LD";
        string pmsCrsProperty = "VALKINT";



        public Authentication Authentication(string pmsToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

           var authentication = new SOAPService.Authentication
           {
                User = pmsUser,
                Password = pmsPassword,
                VendorId = pmsVendorId,
                CrsProperty = pmsCrsProperty,
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
                User = pmsUser,
                Password = pmsPassword,
                VendorId = pmsVendorId
            };

            //response
            SOAPService.AuthenticationResponse authenticationResponse = client.HceAuthenticateAsync(authenticationRequest).Result;
            return authenticationResponse;
        }
    }
}
