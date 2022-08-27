using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.DB.Models.Common
{
    public class JobTaskScheduler
    {
        public int Id { get; set; }
        public string MethodName { get; set; } 
        public DateTime? LastExecutionDate { get; set; } 
    }
}
