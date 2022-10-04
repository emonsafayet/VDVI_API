using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.Dtos.AfasDtos
{
    public class DMFAdministratiesDto : Audit
    {
        public int Administratie_code { get; set; }
        public string Administratie { get; set; }
        public string Administratietype_code { get; set; }
        public string Administratietype { get; set; }

    }
}
