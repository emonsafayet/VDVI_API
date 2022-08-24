using System;
using System.Collections.Generic;
using System.Text; 
using VDVI.DB.Models.Common;

namespace VDVI.Services.IServices
{
    public interface IApmaTaskSchedulerService
    {
        void InsertOrUpdateTaskScheduleDatetime(string methodName, DateTime startDate, DateTime endDate,int flag);
        TaskScheduler GetTaskScheduler(string methodName); 
    }
}
