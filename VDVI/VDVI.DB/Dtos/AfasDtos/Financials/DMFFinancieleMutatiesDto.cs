using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDVI.Repository.Models.AfasModels.Financials
{
    [ConnectorName("DMF_Financiele_Mutaties")]
    public class DMFFinancieleMutatiesDto : Audit, IGetEntity
    {
        public string Omgeving_code { get; set; }

        [JsonProperty("Journaalpost_nr")]
        public virtual int? Journaalpost_nr { get; set; }


        [JsonProperty("Journaalpost_vnr")]
        public virtual int? Journaalpost_vnr { get; set; }


        [JsonProperty("Journaalpost_vnr_vb")]
        public virtual int? Journaalpost_vnr_vb { get; set; }

        [JsonProperty("Dossieritem")]
        public virtual int? Dossieritem { get; set; }


        [JsonProperty("Rekeningnummer")]
        public virtual string Rekeningnummer { get; set; }


        [JsonProperty("Verzamelrekening")]
        public virtual string Verzamelrekening { get; set; }


        [JsonProperty("Rekeningkenmerk")]
        public virtual string Rekeningkenmerk { get; set; }

        [JsonProperty("Rekeningtype")]
        public virtual string Rekeningtype { get; set; }

        [JsonProperty("Consolidatiecode")]
        public virtual string Consolidatiecode { get; set; }


        [JsonProperty("PL_code")]
        public virtual string PL_code { get; set; }

        [JsonProperty("Verbijzondering_bedrijfsonderdeel")]
        public virtual string Verbijzondering_bedrijfsonderdeel { get; set; }


        [JsonProperty("Verbijzondering_afdeling")]
        public virtual string Verbijzondering_afdeling { get; set; }

        [JsonProperty("Verbijzondering_wkr")]
        public virtual string? Verbijzondering_wkr { get; set; }


        [JsonProperty("Verbijzondering_nieuwbouw")]
        public virtual string? Verbijzondering_nieuwbouw { get; set; }

        [JsonProperty("Verbijzondering_medewerker")]
        public virtual string? Verbijzondering_medewerker { get; set; }

        [JsonProperty("Jaar")]
        public virtual int? Jaar { get; set; }


        [JsonProperty("Periode")]
        public virtual int? Periode { get; set; }


        [JsonProperty("Datum_boeking")]
        public virtual DateTime? Datum_boeking { get; set; }


        [JsonProperty("Omschrijving")]
        public virtual string Omschrijving { get; set; }

        [JsonProperty("Boekstuknummer")]
        public virtual string Boekstuknummer { get; set; }

        [JsonProperty("Boekstukdatum")]
        public virtual DateTime? Boekstukdatum { get; set; }


        [JsonProperty("Factuurnummer")]
        public virtual string Factuurnummer { get; set; }

        [JsonProperty("Bedrag")]
        public virtual double? Bedrag { get; set; }


        [JsonProperty("Bedrag_debet")]
        public virtual double? Bedrag_debet { get; set; }


        [JsonProperty("Bedrag_credit")]
        public virtual double? Bedrag_credit { get; set; }

        [JsonProperty("TP_Debet")]
        public virtual double? TP_Debet { get; set; }


        [JsonProperty("TP_Credit")]
        public virtual double? TP_Credit { get; set; }

        [JsonProperty("Code_dagboek")]
        public virtual string Code_dagboek { get; set; }


        [JsonProperty("Transitorische_post")]
        public virtual bool? Transitorische_post { get; set; }
    }
}
