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
        public void InsertTaskScheduleDatetime(TaskScheduler taskScheduler)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"INSERT INTO [dbo].[TaskSchedule](MethodName,StartDate,EndDate)
                                    VALUES (@MethodName	,@StartDate	,@EndDate)";
                    dbConnection.Execute(query, taskScheduler);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
