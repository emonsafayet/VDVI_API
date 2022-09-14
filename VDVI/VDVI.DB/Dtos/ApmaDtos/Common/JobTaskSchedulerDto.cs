using System;

namespace VDVI.DB.Models.Common
{
    public class JobTaskSchedulerDto
    { 
        public string MethodName { get; set; } 
        public DateTime? LastExecutionDate { get; set; } 
        public bool flag { get; set; } 
    }
}
