using Framework.Core.Repository;
using VDVI.AfasRepository.Interfaces;
using VDVI.Repository.AfasRepository.Implementation;

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
        IDMFGrootboekrekeningenRepository DMFGrootboekrekeningenRepository { get; }
        //Finance
        IDMFBeginbalansRepository DMFBeginbalansRepository { get; }
    }
}
