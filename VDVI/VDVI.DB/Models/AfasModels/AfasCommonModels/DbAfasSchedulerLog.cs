using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDVI.Repository.Models.AfasModel
{
    [Table("[dbo].[AfasSchedulerLog]")]
    public class DbAfasSchedulerLog
    {
        public string MethodName { get; set; }
        public DateTime? ExecutionDateTime { get; set; }
    }
}
