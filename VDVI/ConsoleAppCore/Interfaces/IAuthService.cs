using SOAPService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOAPAppCore.Interfaces
{
    public interface IAuthService
    {
        public Authentication Authentication(string pmsToken);
        public AuthenticationResponse AuthenticationResponse();
    }
}
