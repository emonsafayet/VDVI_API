using System;
using System.Collections.Generic;
using System.Text;

namespace VDVI.DB.Models.Common
{
    public class TaskScheduler
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
    }
}
