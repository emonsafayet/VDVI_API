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

namespace VDVI.Repository.ApmaRepository.Implementation
{
    public class SchedulerLogRepository : DapperRepository<DbSchedulerLog>, ISchedulerLogRepository
    {
        private readonly VDVISchedulerDbContext _dbContext;
        private readonly IDapperRepository<DbSchedulerLog> _tblRepository;

        public SchedulerLogRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
            _tblRepository = _dbContext.SchedulerLog;
        } 

        public async Task<SchedulerLogDto> InsertAsync(SchedulerLogDto dto)
        {
            var dbEntity = TinyMapper.Map<DbSchedulerLog>(dto);

            await _tblRepository.InsertAsync(dbEntity);

            return TinyMapper.Map<SchedulerLogDto>(dbEntity);
        }
    }
}
