using System;

namespace VDVI.Repository.DB
{
    public class DbJobTaskScheduler
    {
        public int Id { get; set; }
        public string MethodName { get; set; } 
        public DateTime? LastExecutionDate { get; set; } 
    }
}
