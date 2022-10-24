using Framework.Core.Repository;
using VDVI.AfasRepository.Interfaces; 
using VDVI.Repository.AfasRepository.Implementation;
using VDVI.Repository.ApmaRepository.Implementation;
using VDVI.Repository.DbContext.AfasDbContext;
using VDVI.Repository.Implementation;

namespace VDVI.AfasRepository
{
    public class AfasMasterRepository : ProRepository, IAfasMasterRepositroy
    {
        private readonly AfasDbContext _dbContext;

        public AfasMasterRepository(AfasDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
         
        // Common
        public IAfasSchedulerSetupRepository AfasSchedulerSetupRepository => new AfasSchedulerSetupRepository(_dbContext);
        public IAfasSchedulerLogRepository AfasSchedulerLogRepository => new AfasSchedulerLogRepository(_dbContext);
       
        //Administrator
        public IDMFAdministratiesRepository DMFAdministratiesRepository =>  new DMFAdministratiesRepository(_dbContext);
        //Accounts
        public IDMFGrootboekrekeningenRepository DMFGrootboekrekeningenRepository => new DMFGrootboekrekeningenRepository(_dbContext);

        //Finance
        public IDMFBeginbalansRepository DMFBeginbalansRepository=> new DMFBeginbalansRepository(_dbContext);

        public IDMFFinancieleMutatiesRepository DMFFinancieleMutatiesRepository => new DMFFinancieleMutatiesRepository(_dbContext);

        public IDMFBoekingsdagenMutatiesRepository DMFBoekingsdagenMutatiesRepository => new DMFBoekingsdagenMutatiesRepository(_dbContext); 


    }
}
