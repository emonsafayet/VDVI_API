using System.Data;

namespace Framework.Core.Repository
{
    public class ProRepository : IProRepository
    {
        private readonly ProDbContext _dbContext;
        protected IDbTransaction DbTransaction;

        protected ProRepository(ProDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            DbTransaction = _dbContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                DbTransaction.Commit();
                DbTransaction.Dispose();
            }
            catch
            {
                RollBackTransaction();

            }
        }

        public void RollBackTransaction()
        {
            DbTransaction.Rollback();
            DbTransaction.Dispose();
        }
    }
}
