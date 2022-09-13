using Unity;
using Unity.Lifetime; 
using Framework.Core.Repository; 
using VDVI.Repository.ApmaRepository;
using VDVI.Repository.DbContext.ApmaDbContext; 
using VDVI.Services.Interfaces;  
using Framework.Core.IoC;
using VDVI.Services;
using VDVI.ApmaRepository;
using VDVI.ApmaRepository.Interfaces;
using VDVI.Repository.ApmaRepository.Implementation;

namespace VDVI.Client.IoC
{
    public class UnityDependencyProvider : IDependencyProvider
    {
        public void RegisterDependencies(IUnityContainer container)
        {
            //dependency resolve:

            container.RegisterType<IVDVISchedulerDbContext, VDVISchedulerDbContext>(new SingletonLifetimeManager());

            container.RegisterType<IProRepository, ProRepository>();

            container.RegisterType<IApmaTaskSchedulerService, ApmaTaskSchedulerService>();

            container.RegisterType<IJobTaskSchedulerRepository, JobTaskSchedulerRepository>();

            container.RegisterType<IMasterRepository, MasterRepository>();

            container.RegisterType<IHcsReportManagementSummaryService, HcsReportManagementSummaryService>();

            container.RegisterType<IHcsRoomSummaryService, HcsRoomSummaryService>();

            container.RegisterType<IHcsBIReservationDashboardService, HcsBIReservationDashboardService>();

            container.RegisterType<IHcsLedgerBalanceService, HcsLedgerBalanceService>();
        }
    }
}
