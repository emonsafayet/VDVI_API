using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VDVI.DB.Models.Common;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Dtos.Accounts;
using VDVI.Repository.Dtos.SourceStatistics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class JobTaskSchedulerRepository : DapperRepository<JobTaskSchedulerDto>, IJobTaskSchedulerRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;

        public JobTaskSchedulerRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
        }

        public async Task<string> FindByMethodNameAsync(string methodName)
        {
            DbJobTaskScheduler dbEntities = await _dbContext.JobTaskScheduler.FindAsync(x => x.MethodName == methodName);
            return dbEntities.MethodName;
        }

        public async Task<string>  SaveWithProcAsync(JobTaskSchedulerDto dto)
        {
            var queryResult = await _dbContext.Connection.QueryAsync<string>("sp_hce_InsertOrUpdateTaskScheduleDatetime",
                new
                {
                    dto.MethodName,
                    lastExecutionDatetime = dto.LastExecutionDate,
                    dto.flag
                },
                commandType: CommandType.StoredProcedure);

            return null;
        }
         
    }
}
