using DutchGrit.Afas;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos
{
    public class AfasCrenditalsDto
    {
        public AfasClient clientAA { get; set; }
        public AfasClient clientAC { get; set; }
        public AfasClient clientAD { get; set; }
        public AfasClient clientAE { get; set; }
    }
}
