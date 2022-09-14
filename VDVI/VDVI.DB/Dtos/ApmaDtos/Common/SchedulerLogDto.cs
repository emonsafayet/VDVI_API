using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.Repository.Dtos.ApmaDtos.Common
{
    public class SchedulerLogDto
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public DateTime? ExecutionDateTime { get; set; }       
    }
}
