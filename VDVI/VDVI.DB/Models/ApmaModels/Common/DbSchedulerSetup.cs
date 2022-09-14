using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDVI.Repository.Dtos.ApmaDtos.Common
{
    [Table("[dbo].[SchedulerSetUp]")]
    public class DbSchedulerSetup
    { 
        public string SchedulerName { get; set; }
        public DateTime? LastExecutionDateTime { get; set; }
        public DateTime? NextExecutionDateTime { get; set; }
        public int NextExecutionHour { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
