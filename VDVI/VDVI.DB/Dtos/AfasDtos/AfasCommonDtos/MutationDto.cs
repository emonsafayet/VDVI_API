using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos
{
    public class MutationDto
    {
        public bool IsInitialRecord { get; set; } = false;
        public DateTime? LastRecordDate { get; set; }
    }
}
