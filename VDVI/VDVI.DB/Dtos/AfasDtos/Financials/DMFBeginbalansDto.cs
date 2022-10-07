using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.AfasDtos
{

    [ConnectorName("DMF_Beginbalans")]
    public class DMFBeginbalansDto : Audit, IGetEntity
    {
        public string Omgeving_code { get; set; }

        [JsonProperty("Administratie_code")]
        public virtual int? Administratie_code { get; set; }

        [JsonProperty("Rekeningnummer")]
        public virtual string Rekeningnummer { get; set; }

        [JsonProperty("Jaar")]
        public virtual int? Jaar { get; set; }

        [JsonProperty("Periode")]
        public virtual int? Periode { get; set; }

        [JsonProperty("Datum_boeking")]
        public virtual DateTime? Datum_boeking { get; set; }

        [JsonProperty("Saldo")]
        public virtual double? Saldo { get; set; }
    }
}
