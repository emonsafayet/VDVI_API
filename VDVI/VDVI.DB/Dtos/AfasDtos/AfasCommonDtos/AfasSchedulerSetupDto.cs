using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDVI.Repository.Models;

namespace VDVI.Repository.Models.AfasModels.Dto
{ 
    public class AfasSchedulerSetupDto : Audit
    {
        public string SchedulerName { get; set; }
        public DateTime? LastExecutionDateTime { get; set; }
        public DateTime? NextExecutionDateTime { get; set; }
        public int ExecutionIntervalMins { get; set; }
        public DateTime? BusinessStartDate { get; set; } 
        public bool isActive { get; set; } 
    }
}
