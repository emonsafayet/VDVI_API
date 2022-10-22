using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.Dtos.AfasDtos
{
    [ConnectorName("DMF_Boekingsdagen_Mutaties")]
    public class DMFBoekingsdagenMutatiesDto : Audit, IGetEntity
    {
        /// <summary>
        /// Datum_boeking
        /// </summary>
        [Description("Datum_boeking")]
        [JsonProperty("Datum_boeking")]
        public virtual DateTime? Datum_boeking { get; set; }


        /// <summary>
        /// Datum_gewijzigd
        /// </summary>
        [Description("Datum_gewijzigd")]
        [JsonProperty("Datum_gewijzigd")]
        public virtual DateTime? Datum_gewijzigd { get; set; }
    }
}
