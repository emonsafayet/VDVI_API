using Unity;
using Unity.Lifetime;
using VDVI.DB.Repository;
using SOAPAppCore.Interfaces;
using Framework.Core.Repository;
using SOAPAppCore.Services.Apma;
using VDVI.Repository.ApmaRepository;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Repository.Implementation;
using VDVI.Repository.Interfaces;
using VDVI.Services.Interfaces;
using VDVI.Services.Interfaces.Apma;
using VDVI.Services.Interfaces.Apma.Accounts;
using VDVI.Services.Services;
using VDVI.Services.Services.Apma;
using VDVI.Services.Services.Apma.Accounts;
using Framework.Core.IoC;
using VDVI.Repository.Repository.ApmaRepository.Interfaces.Common;

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

            container.RegisterType<IJobTaskSchedulerRepository, TaskSchedulerRepository>();

            container.RegisterType<IMasterRepository, MasterRepository>();

            container.RegisterType<IHcsReportManagementSummaryService, HcsReportManagementSummaryService>();

            container.RegisterType<IHcsRoomSummaryService, HcsRoomSummaryService>();

            container.RegisterType<IHcsBIReservationDashboardService, HcsBIReservationDashboardService>();

            container.RegisterType<IHcsLedgerBalanceService, HcsLedgerBalanceService>();
        }
    }
}
