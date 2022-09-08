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
using VDVI.Services.Interfaces;
using VDVI.Services.Services;

namespace VDVI.Client.IoC
{
    public class UnityDependencyProvider : IDependencyProvider
    {
        public void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<IVDVISchedulerDbContext, VDVISchedulerDbContext>(new SingletonLifetimeManager());
            container.RegisterType<IProRepository, ProRepository>();
            container.RegisterType<IScheduleManagementRepository, ScheduleManagementRepository>();

            container.RegisterType<IApmaTaskSchedulerService, ApmaTaskSchedulerService>();
            container.RegisterType<IHcsBISourceStatisticsService, HcsBISourceStatisticsService>();


            //dependency resolve: 
            container.RegisterType<IReportManagementSummaryService, ReportManagementSummaryService>();
            container.RegisterType<IHcsReportManagementSummaryRepository, HcsReportManagementSummaryRepository>();
            container.RegisterType<IHcsBISourceStatisticsRepository, HcsBISourceStatisticsRepository>();
            container.RegisterType<ITaskSchedulerRepository, TaskSchedulerRepository>();


            container.RegisterType<IReportManagementSummaryService, ReportManagementSummaryService>();


        }
    }
}
