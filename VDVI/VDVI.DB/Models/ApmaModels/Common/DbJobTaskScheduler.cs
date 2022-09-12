using System;

namespace VDVI.DB.Models.Common
{
    public class DbJobTaskScheduler
    {
        public int Id { get; set; }
        public string MethodName { get; set; } 
        public DateTime? LastExecutionDate { get; set; } 
    }
}
