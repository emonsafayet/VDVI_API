using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DutchGrit.Afas;
using VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos;

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
        public AfasCrenditalsDto GetAfmaConnectors()
        {
            AfasCrenditalsDto afasCrenditalsDto = new AfasCrenditalsDto();
            afasCrenditalsDto.clientAA = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AA").Value);
            afasCrenditalsDto.clientAC = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AC").Value);
            afasCrenditalsDto.clientAD = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AD").Value);
            afasCrenditalsDto.clientAE = new AfasClient(85007, _config.GetSection("AfasAuthenticationCrendital").GetSection("AE").Value);

            return afasCrenditalsDto;
        }
    }
}
