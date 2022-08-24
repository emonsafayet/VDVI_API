using System;
using System.Collections.Generic;
using System.Text; 
using VDVI.DB.IRepository;
using VDVI.DB.Models.Common;
using VDVI.Services.IServices;

namespace VDVI.Services.Services
{
    public class ApmaTaskSchedulerService : IApmaTaskSchedulerService
    {
        public ITaskSchedulerRepository _taskScheduler;
        public ApmaTaskSchedulerService(ITaskSchedulerRepository taskScheduler)
        {
            _taskScheduler = taskScheduler;
        }

        public  TaskScheduler GetTaskScheduler(string methodName)
        {
            try
            {
                return _taskScheduler.GetTaskScheduler(methodName);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public void InsertOrUpdateTaskScheduleDatetime(string methodName,DateTime startDate,DateTime endDate,int flag)
        {
            try
            {
                _taskScheduler.InsertOrUpdateTaskScheduleDatetime( methodName,  startDate,  endDate,  flag);
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
    }
}
