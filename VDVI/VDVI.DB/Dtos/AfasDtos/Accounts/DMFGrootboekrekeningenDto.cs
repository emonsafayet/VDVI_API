using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.AfasDtos
{
    [ConnectorName("DMF_Grootboekrekeningen")]
    public class DMFGrootboekrekeningenDto : Audit, IGetEntity
    {
        public string Omgeving_code { get; set; }

        [JsonProperty("Rekeningnummer")]
        public virtual string Rekeningnummer { get; set; }

        [JsonProperty("Grootboekrekening")]
        public virtual string Grootboekrekening { get; set; }

        [JsonProperty("Rekeningtype")]
        public virtual string Rekeningtype { get; set; }

        [JsonProperty("Rekeningkenmerk")]
        public virtual string Rekeningkenmerk { get; set; }


        [JsonProperty("Consolidatiecode")]
        public virtual string Consolidatiecode { get; set; }

        [JsonProperty("PL_code")]
        public virtual string PL_code { get; set; }

        [JsonProperty("Verbijzondering_bedrijfsonderdeel")]
        public virtual string Verbijzondering_bedrijfsonderdeel { get; set; }

        [JsonProperty("Verbijzondering_afdeling")]
        public virtual string Verbijzondering_afdeling { get; set; }


        [JsonProperty("Verbijzondering_wkr")]
        public virtual string Verbijzondering_wkr { get; set; }


        [JsonProperty("Verbijzondering_nieuwbouw")]
        public virtual string Verbijzondering_nieuwbouw { get; set; }


        [JsonProperty("Verbijzondering_medewerker")]
        public virtual string Verbijzondering_medewerker { get; set; }
    }
}
