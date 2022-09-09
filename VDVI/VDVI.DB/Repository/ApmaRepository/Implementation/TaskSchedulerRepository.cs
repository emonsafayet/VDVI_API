using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using VDVI.DB.IRepository;
using VDVI.DB.Models.Common;

namespace VDVI.DB.Repository
{
    public class TaskSchedulerRepository : ITaskSchedulerRepository
    {
        protected readonly IConfiguration _config;

        public TaskSchedulerRepository(IConfiguration config)
        {
            _config = config;
        }

        //create an IDbConnection object called Connection to inito database connection
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("ApmaDb"));
            }
            
        }  
        public JobTaskScheduler GetTaskScheduler(string methodName)
        {
            try
            {
                var param = new DynamicParameters();

                using (IDbConnection dbConnection = Connection)
                { 
                    dbConnection.Open();
                    string query = @"SELECT TOP 1 lastExecutionDate FROM dbo.JobTaskScheduler WHERE MethodName=@methodName ORDER BY lastExecutionDate,EntryDate desc  ";
                    return dbConnection.QueryFirstOrDefault<JobTaskScheduler>(query, new { methodName = methodName });                     
                } 
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertOrUpdateTaskScheduleDatetime(string methodName, DateTime lastExecutionDatetime, int flag)
        {
            var param = new DynamicParameters();

            using (IDbConnection dbConnection = Connection)
            {
                param.Add("@MethodName", methodName); 
                param.Add("@lastExecutionDatetime", lastExecutionDatetime);
                param.Add("@flag", flag);

                dbConnection.Open();
                dbConnection.Execute("sp_hce_InsertOrUpdateTaskScheduleDatetime", 
                    param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
