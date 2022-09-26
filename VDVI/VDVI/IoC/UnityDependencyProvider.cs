using Unity;
using Unity.Lifetime;
using Framework.Core.Repository;
using VDVI.Repository.DbContext.ApmaDbContext;
using VDVI.Services.Interfaces;
using Framework.Core.IoC;
using VDVI.Services;
using VDVI.ApmaRepository;
using VDVI.Services.Services.ApmaServices;

namespace VDVI.Client.IoC
{
    public class UnityDependencyProvider : IDependencyProvider
    {
        public void RegisterDependencies(IUnityContainer container)
        {
            //dependency resolve:

            container.RegisterType<IVDVISchedulerDbContext, VDVISchedulerDbContext>(new SingletonLifetimeManager());

            container.RegisterType<IProRepository, ProRepository>();           

            container.RegisterType<IMasterRepository, MasterRepository>();

            container.RegisterType<IApmaTaskSchedulerService, ApmaTaskSchedulerService>();
            container.RegisterType<ISchedulerSetupService, SchedulerSetupService>(); 
            container.RegisterType<ISchedulerLogService, SchedulerLogService>(); 
            
            //Parent
            container.RegisterType<IHcsReportManagementSummaryService, HcsReportManagementSummaryService>();
            container.RegisterType<IHcsBIReservationDashboardHistoryService, HcsBIReservationDashboardHistoryService>();
            container.RegisterType<IHcsBIReservationDashboardFutureService, HcsBIReservationDashboardFutureService>();
            container.RegisterType<IHcsBIRatePlanStatisticsHistoryService, HcsBIRatePlanStatisticsHistoryService>();
            container.RegisterType<IHcsBIRatePlanStatisticsFutureService, HcsBIRatePlanStatisticsFutureService>();            
            container.RegisterType<IHcsBISourceStatisticsHistoryService, HcsBISourceStatisticsHistoryService>();
            container.RegisterType<IHcsBISourceStatisticsFutureService, HcsBISourceStatisticsFutureService>();
            container.RegisterType<IHcsGetDailyHistoryService, HcsGetDailyHistoryService>();
            container.RegisterType<IHcsRoomSummaryService, HcsRoomSummaryService>();
            container.RegisterType<IHcsLedgerBalanceService, HcsLedgerBalanceService>();


            //Child
            container.RegisterType<IHcsRatePlanStatisticsHistoryService, HcsRatePlanStatisticsHistoryService>();
            container.RegisterType<IHcsSourceStasticsHistoryService, HcsSourceStasticsHistoryService>();            
            container.RegisterType<IHcsBIOccupancyHistoryService, HcsBIOccupancyHistoryService>();
            container.RegisterType<IHcsBIOccupancyFutureService, HcsBIOccupancyFutureService>();
            container.RegisterType<IHcsBIReservationHistoryService, HcsBIReservationHistoryService>();
            container.RegisterType<IHcsBIReservationFutureService, HcsBIReservationFutureService>();
            container.RegisterType<IHcsBIRoomsHistoryService, HcsBIRoomsHistoryService>();
            container.RegisterType<IHcsBIRoomsFutureService, HcsBIRoomsFutureService>();
            container.RegisterType<IHcsBIRevenueHistoryService, HcsBIRevenueHistoryService>();
            container.RegisterType<IHcsBIRevenueFutureService, HcsBIRevenueFutureService>();
            container.RegisterType<IHcsDailyHistoryService, HcsDailyHistoryService>();   
            container.RegisterType<IHcsSourceStasticsFutureService, HcsSourceStasticsFutureService>();
            container.RegisterType<IHcsRatePlanStatisticsFutureService, HcsRatePlanStatisticsFutureService>();
        }
    }
}
