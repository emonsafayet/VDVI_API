using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.AfasModels
{
    [Table("[dmf].[Financiele_Mutaties]")]
    public class DbDMFFinancieleMutaties : Audit
    {
        public string Omgeving_code { get; set; } 
        public virtual int? Administratie_code { get; set; }
        public virtual int? Journaalpost_nr { get; set; }
        public virtual int? Journaalpost_vnr { get; set; }
        public virtual int? Journaalpost_vnr_vb { get; set; }
        public virtual int? Dossieritem { get; set; }
        public virtual string Rekeningnummer { get; set; }
        public virtual string Verzamelrekening { get; set; }
        public virtual string Rekeningkenmerk { get; set; }
        public virtual string Rekeningtype { get; set; }
        public virtual string Consolidatiecode { get; set; }
        public virtual string PL_code { get; set; }
        public virtual string Verbijzondering_bedrijfsonderdeel { get; set; }
        public virtual string Verbijzondering_afdeling { get; set; }
        public virtual string? Verbijzondering_wkr { get; set; }
        public virtual string? Verbijzondering_nieuwbouw { get; set; }
        public virtual string? Verbijzondering_medewerker { get; set; }
        public virtual int? Jaar { get; set; }
        public virtual int? Periode { get; set; }
        public virtual DateTime? Datum_boeking { get; set; }
        public virtual string Omschrijving { get; set; }
        public virtual string Boekstuknummer { get; set; }
        public virtual DateTime? Boekstukdatum { get; set; }
        public virtual string Factuurnummer { get; set; }
        public virtual double? Bedrag { get; set; }
        public virtual double? Bedrag_debet { get; set; }
        public virtual double? Bedrag_credit { get; set; }
        public virtual double? TP_Debet { get; set; }
        public virtual double? TP_Credit { get; set; }
        public virtual string Code_dagboek { get; set; }
        public virtual bool? Transitorische_post { get; set; }
    }
}
