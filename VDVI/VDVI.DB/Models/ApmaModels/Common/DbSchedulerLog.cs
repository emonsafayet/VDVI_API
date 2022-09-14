using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDVI.Repository.Dtos.ApmaDtos.Common
{
    [Table("[dbo].[SchedulerLog]")]
    public class DbSchedulerLog
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public DateTime? ExecutionDateTime { get; set; }       
    }
}
