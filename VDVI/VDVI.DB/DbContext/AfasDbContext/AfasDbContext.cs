using Framework.Core.Repository;
using MicroOrm.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Nelibur.ObjectMapper; 
using System.Collections.Generic; 
using System.Data.SqlClient;
using VDVI.DB.Dtos;
using VDVI.Repository.Models.AfasModel;
using VDVI.Repository.Models.AfasModels.Dto;

namespace VDVI.Repository.DbContext.AfasDbContext
{
    public class AfasDbContext : ProDbContext, IAfasDbContext
    {
        private IDapperRepository<DbDMFAdministraties> _administraties;
        private IDapperRepository<DbAfasSchedulerSetup> _afasSchedulerSetup;
        private IDapperRepository<DbAfasSchedulerLog> _afasSchedulerLog;

        protected AfasDbContext(IConfiguration configuration) : base(new SqlConnection(configuration["ConnectionStrings:AfasDb"]))
        {
            // Dto to Db - Single
            TinyMapper.Bind<DMFAdministratiesDto, DbDMFAdministraties>();
            TinyMapper.Bind<AfasSchedulerSetupDto, DbAfasSchedulerSetup>();
            TinyMapper.Bind<AfasSchedulerLogDto, DbAfasSchedulerLog>();

            // Dto to Db List
            TinyMapper.Bind<List<DMFAdministratiesDto>, List<DbDMFAdministraties>>();
        }
        public IDapperRepository<DbDMFAdministraties> Administraties => _administraties ??= new DapperRepository<DbDMFAdministraties>(Connection);

        public IDapperRepository<DbAfasSchedulerSetup> AfasSchedulerSetup => _afasSchedulerSetup ??= new DapperRepository<DbAfasSchedulerSetup>(Connection);

        public IDapperRepository<DbAfasSchedulerLog> AfasSchedulerLog => _afasSchedulerLog ??= new DapperRepository<DbAfasSchedulerLog>(Connection);
    }
}
