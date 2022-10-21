using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.AfasDtos
{
    [ConnectorName("DMF_Financiele_Mutaties")] 
    public class DMFFinancieleMutatiesDto : Audit,IGetEntity
    {
        public string Omgeving_code { get; set; }

        [Description("Administratie_code")]
        [JsonProperty("Administratie_code")]
        public virtual int? Administratie_code { get; set; }


        /// <summary>
        /// Journaalpost_nr
        /// </summary>
        [Description("Journaalpost_nr")]
        [JsonProperty("Journaalpost_nr")]
        public virtual int? Journaalpost_nr { get; set; }


        /// <summary>
        /// Journaalpost_vnr
        /// </summary>
        [Description("Journaalpost_vnr")]
        [JsonProperty("Journaalpost_vnr")]
        public virtual int? Journaalpost_vnr { get; set; }


        /// <summary>
        /// Journaalpost_vnr_vb
        /// </summary>
        [Description("Journaalpost_vnr_vb")]
        [JsonProperty("Journaalpost_vnr_vb")]
        public virtual int? Journaalpost_vnr_vb { get; set; }


        /// <summary>
        /// Dossieritem
        /// </summary>
        [Description("Dossieritem")]
        [JsonProperty("Dossieritem")]
        public virtual int? Dossieritem { get; set; }


        /// <summary>
        /// Rekeningnummer
        /// </summary>
        [Description("Rekeningnummer")]
        [JsonProperty("Rekeningnummer")]
        public virtual string Rekeningnummer { get; set; }


        /// <summary>
        /// Verzamelrekening
        /// </summary>
        [Description("Verzamelrekening")]
        [JsonProperty("Verzamelrekening")]
        public virtual string Verzamelrekening { get; set; }


        /// <summary>
        /// Rekeningkenmerk
        /// </summary>
        [Description("Rekeningkenmerk")]
        [JsonProperty("Rekeningkenmerk")]
        public virtual string Rekeningkenmerk { get; set; }


        /// <summary>
        /// Rekeningtype
        /// </summary>
        [Description("Rekeningtype")]
        [JsonProperty("Rekeningtype")]
        public virtual string Rekeningtype { get; set; }


        /// <summary>
        /// Consolidatiecode
        /// </summary>
        [Description("Consolidatiecode")]
        [JsonProperty("Consolidatiecode")]
        public virtual string Consolidatiecode { get; set; }


        /// <summary>
        /// PL_code
        /// </summary>
        [Description("PL_code")]
        [JsonProperty("PL_code")]
        public virtual string PL_code { get; set; }


        /// <summary>
        /// Verbijzondering_bedrijfsonderdeel
        /// </summary>
        [Description("Verbijzondering_bedrijfsonderdeel")]
        [JsonProperty("Verbijzondering_bedrijfsonderdeel")]
        public virtual string Verbijzondering_bedrijfsonderdeel { get; set; }


        /// <summary>
        /// Verbijzondering_afdeling
        /// </summary>
        [Description("Verbijzondering_afdeling")]
        [JsonProperty("Verbijzondering_afdeling")]
        public virtual string Verbijzondering_afdeling { get; set; }

        /// <summary>
        /// Verbijzondering_wkr
        /// </summary>
        [Description("Verbijzondering_wkr")]
        [JsonProperty("Verbijzondering_wkr")]
        public virtual string? Verbijzondering_wkr { get; set; }


        /// <summary>
        /// Verbijzondering_nieuwbouw
        /// </summary>
        [Description("Verbijzondering_nieuwbouw")]
        [JsonProperty("Verbijzondering_nieuwbouw")]
        public virtual string? Verbijzondering_nieuwbouw { get; set; }

        /// <summary>
        /// Verbijzondering_medewerker
        /// Manually added after autogeneration of class. This element is only available in AE environment
        /// </summary>
        [Description("Verbijzondering_medewerker")]
        [JsonProperty("Verbijzondering_medewerker")]
        public virtual string? Verbijzondering_medewerker { get; set; }

        /// <summary>
        /// Jaar
        /// </summary>
        [Description("Jaar")]
        [JsonProperty("Jaar")]
        public virtual int? Jaar { get; set; }


        /// <summary>
        /// Periode
        /// </summary>
        [Description("Periode")]
        [JsonProperty("Periode")]
        public virtual int? Periode { get; set; }


        /// <summary>
        /// Datum_boeking
        /// </summary>
        [Description("Datum_boeking")]
        [JsonProperty("Datum_boeking")]
        public virtual DateTime? Datum_boeking { get; set; }


        /// <summary>
        /// Omschrijving
        /// </summary>
        [Description("Omschrijving")]
        [JsonProperty("Omschrijving")]
        public virtual string Omschrijving { get; set; }


        /// <summary>
        /// Boekstuknummer
        /// </summary>
        [Description("Boekstuknummer")]
        [JsonProperty("Boekstuknummer")]
        public virtual string Boekstuknummer { get; set; }


        /// <summary>
        /// Boekstukdatum
        /// </summary>
        [Description("Boekstukdatum")]
        [JsonProperty("Boekstukdatum")]
        public virtual DateTime? Boekstukdatum { get; set; }


        /// <summary>
        /// Factuurnummer
        /// </summary>
        [Description("Factuurnummer")]
        [JsonProperty("Factuurnummer")]
        public virtual string Factuurnummer { get; set; }


        /// <summary>
        /// Bedrag
        /// </summary>
        [Description("Bedrag")]
        [JsonProperty("Bedrag")]
        public virtual double? Bedrag { get; set; }


        /// <summary>
        /// Bedrag debet
        /// </summary>
        [Description("Bedrag debet")]
        [JsonProperty("Bedrag_debet")]
        public virtual double? Bedrag_debet { get; set; }


        /// <summary>
        /// Bedrag credit
        /// </summary>
        [Description("Bedrag credit")]
        [JsonProperty("Bedrag_credit")]
        public virtual double? Bedrag_credit { get; set; }


        /// <summary>
        /// TP Debet
        /// </summary>
        [Description("TP Debet")]
        [JsonProperty("TP_Debet")]
        public virtual double? TP_Debet { get; set; }


        /// <summary>
        /// TP Credit
        /// </summary>
        [Description("TP Credit")]
        [JsonProperty("TP_Credit")]
        public virtual double? TP_Credit { get; set; }


        /// <summary>
        /// Code dagboek
        /// </summary>
        [Description("Code dagboek")]
        [JsonProperty("Code_dagboek")]
        public virtual string Code_dagboek { get; set; }


        /// <summary>
        /// Transitorische post
        /// </summary>
        [Description("Transitorische post")]
        [JsonProperty("Transitorische_post")]
        public virtual bool? Transitorische_post { get; set; }
    }
}
