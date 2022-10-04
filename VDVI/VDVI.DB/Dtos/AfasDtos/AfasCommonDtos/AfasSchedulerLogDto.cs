using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDVI.Repository.Models.AfasModels.Dto
{ 
    public class AfasSchedulerLogDto
    {
        public string MethodName { get; set; }
        public DateTime? ExecutionDateTime { get; set; }
    }
}
