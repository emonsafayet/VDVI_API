using Framework.Core.Repository;
using VDVI.AfasRepository.Interfaces; 

namespace VDVI.AfasRepository
{
    public interface IAfasMasterRepositroy : IProRepository
    {
        //Common
        IAfasSchedulerSetupRepository AfasSchedulerSetupRepository { get; }
        IAfasSchedulerLogRepository AfasSchedulerLogRepository { get; }

        //Administration
        IDMFAdministratiesRepository DMFAdministratiesRepository { get; }
        //Accounts

        //Finance
    }
}
