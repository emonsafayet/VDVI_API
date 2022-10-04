using Framework.Core.Repository;
using MicroOrm.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using VDVI.DB.Dtos;
using VDVI.Repository.AfasDtos;
using VDVI.Repository.DB;
using VDVI.Repository.Dtos.AfasDtos;

namespace VDVI.Repository.DbContext.AfasDbContext
{
    public class AfasDbContext : ProDbContext, IAfasDbContext
    {
        private IDapperRepository<DbDMFAdministraties> _administraties;
        protected AfasDbContext(IConfiguration configuration) : base(new SqlConnection(configuration["ConnectionStrings:AfasDb"]))
        {
            // Dto to Db - Single
            TinyMapper.Bind<DMFAdministratiesDto, DbDMFAdministraties>();

            // Dto to Db List
            TinyMapper.Bind<List<DMFAdministratiesDto>, List<DbDMFAdministraties>>();
        }
        public IDapperRepository<DbDMFAdministraties> Administraties => _administraties ??= new DapperRepository<DbDMFAdministraties>(Connection);
    }
}
