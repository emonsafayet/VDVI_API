using SOAPService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOAPAppCore.Interfaces
{
    public interface IApmaAuthService
    {
        public Authentication Authentication(string pmsToken);
        public AuthenticationResponse AuthenticationResponse();
    }
}
