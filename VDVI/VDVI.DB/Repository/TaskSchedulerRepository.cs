using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text; 
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

        public TaskScheduler GetTaskScheduler(string methodName)
        { 
         
            try
            {
                var param = new DynamicParameters();

                using (IDbConnection dbConnection = Connection)
                { 
                    dbConnection.Open();
                    string query = @"SELECT EndDate FROM dbo.TaskSchedule WHERE MethodName=@methodName ";
                    return dbConnection.QueryFirstOrDefault<TaskScheduler>(query, new { methodName = methodName });                     
                } 
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertOrUpdateTaskScheduleDatetime(string methodName, DateTime startDate, DateTime endDate, int flag)
        {
            var param = new DynamicParameters();

            using (IDbConnection dbConnection = Connection)
            {
                param.Add("@MethodName", methodName);
                param.Add("@StartDate", startDate);
                param.Add("@EndDate", endDate);
                param.Add("@flag", flag);

                dbConnection.Open();
                dbConnection.Execute("sp_hce_InsertOrUpdateTaskScheduleDatetime", 
                    param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
