using Framework.Core.IoC;
using Framework.Core.Repository;
using SOAPAppCore.Interfaces;
using SOAPAppCore.Services.Apma;
using Unity;
using Unity.Lifetime;
using VDVI.DB.IRepository;
using VDVI.DB.Repository;
using VDVI.Repository.DbContext;
using VDVI.Repository.Repository;
using VDVI.Repository.Repository.Implementation;
using VDVI.Repository.Repository.Interfaces;
using VDVI.Services.Interfaces;
using VDVI.Services.Interfaces.Apma.Accounts;
using VDVI.Services.Services;
using VDVI.Services.Services.Apma.Accounts;

namespace VDVI.Client.IoC
{
    public class UnityDependencyProvider : IDependencyProvider
    {
        public void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<IVDVISchedulerDbContext, VDVISchedulerDbContext>(new SingletonLifetimeManager());
            container.RegisterType<IProRepository, ProRepository>();
            container.RegisterType<IHcsReportManagementLedgerBalanceService, HcsReportManagementLedgerBalanceService>();
            container.RegisterType<IScheduleManagementRepository, ScheduleManagementRepository>();
            container.RegisterType<IApmaTaskSchedulerService, ApmaTaskSchedulerService>();

            //dependency resolve: 
            container.RegisterType<IHcsReportManagementSummaryService, HcsReportManagementSummaryService>();
            container.RegisterType<IHcsReportManagementRoomSummaryService, HcsReportManagementRoomSummaryService>();
            container.RegisterType<IRoomSummaryRepository, HcsRoomSummaryRepository>();
            container.RegisterType<IHcsReportManagementSummaryRepository, HcsReportManagementSummaryRepository>();
            container.RegisterType<IHcsBISourceStatisticsRepository, HcsBISourceStatisticsRepository>();
            container.RegisterType<ITaskSchedulerRepository, TaskSchedulerRepository>();
        }
    }
}
