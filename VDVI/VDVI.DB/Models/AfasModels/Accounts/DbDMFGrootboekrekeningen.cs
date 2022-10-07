using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.AfasModels
{
    [Table("[dmf].[Grootboekrekeningen]")]
    public class DbDMFGrootboekrekeningen : Audit
    {
        public string Omgeving_code { get; set; }
        public virtual string Rekeningnummer { get; set; }
        public virtual string Grootboekrekening { get; set; }
        public virtual string Rekeningtype { get; set; }
        public virtual string Rekeningkenmerk { get; set; }
        public virtual string Consolidatiecode { get; set; }
        public virtual string PL_code { get; set; }
        public virtual string Verbijzondering_bedrijfsonderdeel { get; set; }
        public virtual string Verbijzondering_afdeling { get; set; }
        public virtual string Verbijzondering_wkr { get; set; }
        public virtual string Verbijzondering_nieuwbouw { get; set; }
        public virtual string Verbijzondering_medewerker { get; set; }
    }
}
