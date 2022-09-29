using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Text;
using VDVI.Repository.Models.Common;

namespace VDVI.DB.Dtos
{
    public class SchedulerSetupDto : Audit
    {
        public string SchedulerName { get; set; }
        public DateTime? LastExecutionDateTime { get; set; }
        public DateTime? NextExecutionDateTime { get; set; } 
        public int ExecutionIntervalMins { get; set; }
        public DateTime? BusinessStartDate { get; set; }
        public bool isFuture { get; set; }
        public bool isActive { get; set; }
        public int DayDifference { get; set; }

    }
}
