using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DutchGrit.Afas;

namespace VDVI.Services.Services.BaseService
{
    public class AfasBaseService
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        public IConfiguration _config;

        public AfasBaseService()
        {
            configurationBuilder.AddJsonFile("AppSettings.json");
            _config = configurationBuilder.Build();
        }
        public AfasClient clientAA;
        public AfasClient clientAC;
        public AfasClient clientAD;
        public AfasClient clientAE;
         
        public void GetAfmaConnectors()
        {
            clientAA = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AA").Value);
            clientAC = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AC").Value);
            clientAD = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AD").Value);
            clientAE = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AE").Value);
        }  
    }
}
