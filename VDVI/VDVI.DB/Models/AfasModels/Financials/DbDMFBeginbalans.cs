using System;
using System.ComponentModel.DataAnnotations.Schema;
using VDVI.Repository.Models;

namespace VDVI.Repository.AfasModels
{
    [Table("[dmf].[Beginbalans]")]
    public class DbDMFBeginbalans : Audit
    {
        public string Omgeving_code { get; set; }
        public virtual int? Administratie_code { get; set; }
        public virtual string Rekeningnummer { get; set; }
        public virtual int? Jaar { get; set; }
        public virtual int? Periode { get; set; }
        public virtual DateTime? Datum_boeking { get; set; }
        public virtual double? Saldo { get; set; }
    }
}
