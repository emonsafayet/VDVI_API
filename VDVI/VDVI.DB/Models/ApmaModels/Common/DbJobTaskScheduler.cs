using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDVI.Repository.DB
{
    [Table("[dbo].[JobTaskScheduler]")]
    public class DbJobTaskScheduler
    { 
        public string MethodName { get; set; } 
        public DateTime? LastExecutionDate { get; set; } 
    }
}
