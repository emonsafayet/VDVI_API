using DutchGrit.Afas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.Models.AfasModel
{
    [Table("[dmf].[Administraties]")]
    public class DbDMFAdministraties : IGetEntity
    {
        public int Administratie_code { get; set; }
        public string Administratie { get; set; }
        public string Administratietype_code { get; set; }
        public string Administratietype { get; set; }

        public long CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

    }
}
