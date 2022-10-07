using DutchGrit.Afas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.Models.AfasModel
{
    [Table("[dmf].[Administraties]")]
    public class DbDMFAdministraties : Audit
    {
        public string Omgeving_code { get; set; }
        public int Administratie_code { get; set; }
        public string Administratie { get; set; }
        public string Administratietype_code { get; set; }
        public string Administratietype { get; set; }

    }
}

