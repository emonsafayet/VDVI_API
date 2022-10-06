using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.DB.Dtos
{
    [ConnectorName("DMF_Administraties")]
    public class DMFAdministratiesDto : Audit, IGetEntity
    {
        [JsonProperty("Administratie_code")]
        public int Administratie_code { get; set; }

        [JsonProperty("Administratie")]
        public string Administratie { get; set; }

        [JsonProperty("Administratietype_code")]
        public string Administratietype_code { get; set; }

        [JsonProperty("Administratietype")]
        public string Administratietype { get; set; }

    }
}
