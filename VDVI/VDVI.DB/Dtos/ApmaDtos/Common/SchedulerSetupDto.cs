using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Models.Common;

namespace VDVI.Repository.Dtos.ApmaDtos.Common
{
    public class SchedulerSetupDto : Audit
    {
        public string SchedulerName { get; set; }
        public DateTime? LastExecutionDateTime { get; set; }
        public DateTime? NextExecutionDateTime { get; set; } 
        public int NextExecutionHour { get; set; }
        public DateTime? BusinessStartDate { get; set; }
        public bool isFuture { get; set; }
        public bool isActive { get; set; }
        public int DayDifference { get; set; }

    }
}
