using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DutchGrit.Afas;
using VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;
using System.Linq;
using VDVI.Repository.Dtos.AfasDtos.Administrations;

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
            afasCrenditalsDto.clientAA = new AfasClient(85007, _config.GetSection("AfasAuthenticationCredential").GetSection("AA").Value);
            afasCrenditalsDto.clientAC = new AfasClient(85007, _config.GetSection("AfasAuthenticationCredential").GetSection("AC").Value);
            afasCrenditalsDto.clientAD = new AfasClient(85007, _config.GetSection("AfasAuthenticationCredential").GetSection("AD").Value);
            afasCrenditalsDto.clientAE = new AfasClient(85007, _config.GetSection("AfasAuthenticationCredential").GetSection("AE").Value);

            return afasCrenditalsDto;
        }
        public async Task<AdministrativeDto> AdministrativeList()
        {
            var getConnector = GetAfmaConnectors();
            AdministrativeDto administrative =new AdministrativeDto();

            //Netherlands (=Dutch)=aa  | Spain =ac| Bonaire =ad | Belgium=ae
            administrative._AA = await getConnector.clientAA.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();
            administrative._AC = await getConnector.clientAC.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();
            administrative._AD = await getConnector.clientAD.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();
            administrative._AE = await getConnector.clientAE.Query<DMFAdministratiesDto>().Skip(-1).Take(-1).OrderBy(x => x.Administratie_code).GetAsync();
             
            return administrative;

        }
    }
}
