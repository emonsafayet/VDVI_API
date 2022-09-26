using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDVI.Repository.DB
{
    [Table("[dbo].[SchedulerLog]")]
    public class DbSchedulerLog
    { 
        public string MethodName { get; set; }
        public DateTime? ExecutionDateTime { get; set; }       
    }
}
