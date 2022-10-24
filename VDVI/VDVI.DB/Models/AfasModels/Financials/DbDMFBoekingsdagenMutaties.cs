using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.AfasModels
{ 
    public class DbDMFBoekingsdagenMutaties : Audit
    {

        public virtual string Omgeving_code { get; set; }         

        [Description("Datum_boeking")]
        [JsonProperty("Datum_boeking")]
        public virtual DateTime? Datum_boeking { get; set; } 
        
        [Description("Datum_gewijzigd")]
        [JsonProperty("Datum_gewijzigd")]
        public virtual DateTime? Datum_gewijzigd { get; set; }
    }
}
