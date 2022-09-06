namespace Framework.Core.Repository
{
    public interface IProRepository
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
    }
}