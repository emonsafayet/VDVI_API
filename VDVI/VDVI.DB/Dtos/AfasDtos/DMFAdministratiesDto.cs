using DutchGrit.Afas;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.DB.Dtos
{
    [ConnectorName("DMF_Administraties")]
    public class DMFAdministratiesDto : Audit, IGetEntity
    {
        public string Omgeving_code  { get; set; }

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
