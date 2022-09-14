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
using VDVI.Repository.DB;
using VDVI.Repository.Dtos.ApmaDtos.Common;
using VDVI.DB.Dtos;

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class SchedulerSetupRepository : DapperRepository<DbSchedulerSetup>, ISchedulerSetupRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbSchedulerSetup> _tblRepository;
        public SchedulerSetupRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.SchedulerSetup;
        }

        public async Task<DbSchedulerSetup> FindByMethodNameAsync(string methodName)
        {
            DbSchedulerSetup dbEntities = await _tblRepository.FindAsync(x => x.SchedulerName == methodName);
            return dbEntities;
        }


        public async Task<SchedulerSetupDto> UpdateAsync(SchedulerSetupDto dto)
        {
            var entities = TinyMapper.Map<DbSchedulerSetup>(dto);

            await _tblRepository.UpdateAsync(entities);

            return dto;
        }


        public async Task<string>  SaveWithProcAsync(SchedulerSetupDto dto)
        {
            var queryResult = await _dbContext.Connection.QueryAsync<string>("--writeprocedure--",
                new
                {
                    SchedulerName = dto.SchedulerName,
                    LastExecutionDateTime = dto.LastExecutionDateTime,
                    NextExecutionDateTime = dto.NextExecutionDateTime,
                    NextExecutionHour=dto.NextExecutionHour
                },
                commandType: CommandType.StoredProcedure);

            return queryResult.ToString();
        }
         
    }
}
