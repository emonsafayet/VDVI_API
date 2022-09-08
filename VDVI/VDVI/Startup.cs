using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SOAPAppCore.Interfaces;
using SOAPAppCore.Services;
using SOAPAppCore.Services.Apma;
using System;
using VDVI.DB.IRepository;
using VDVI.DB.Repository;
using VDVI.Repository.Repository.Implementation;
using VDVI.Repository.Repository.Interfaces;
using VDVI.Services.Interfaces;
using VDVI.Services.Interfaces.Apma;
using VDVI.Services.Services;
using VDVI.Services.Services.Apma;

namespace VDVI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services

            services.AddScoped<IApmaTaskSchedulerService, ApmaTaskSchedulerService>();
            services.AddTransient<IHcsReportManagementSummaryService, HcsReportManagementSummaryService>();
            services.AddScoped<IHcsBISourceStatisticsService, HcsBISourceStatisticsService>();
            services.AddScoped<IHcsBIRatePlanStatisticsService, HcsBIRatePlanStatisticsService>(); 
            services.AddScoped<IHcsBIReservationDashboardService, HcsBIReservationDashboardService>(); 


            //dependency resolve: 
    
            services.AddTransient<IHcsReportManagementSummaryRepository, HcsReportManagementSummaryRepository>();
            services.AddTransient<IHcsBISourceStatisticsRepository, HcsBISourceStatisticsRepository>();
            services.AddTransient<IHcsBIRatePlanStatisticsRepository, HcsBIRatePlanStatisticsRepository>();
            services.AddTransient<IHcsBIReservationDashboardRepository, HcsBIReservationDashboardRepository>();

            services.AddTransient<ITaskSchedulerRepository, TaskSchedulerRepository>();

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
            });
            //Hangfire
            services.AddHangfire(config =>
                       config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                       .UseSimpleAssemblyNameTypeSerializer()
                       .UseDefaultTypeSerializer()
                       .UseSqlServerStorage(Configuration.GetConnectionString("ApmaDb")
                       ));
            services.AddHangfireServer();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
                        , IConfiguration configuration,
                        IBackgroundJobClient backgroundJobClient,
                        IRecurringJobManager recurringJobManager,
                        IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
                });
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(async endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Scheduled Jobs"
            });

            recurringJobManager.AddOrUpdate(
                  "InsertHcsReportManagementSummaryJob",
                  () => serviceProvider.GetService<IApmaTaskSchedulerService>().SummaryScheduler("HcsReportManagementSummary"),
                  configuration["ApmaHangfireJobSchedulerTime:HcsReportManagementSummary"], TimeZoneInfo.Utc
                  );
            recurringJobManager.AddOrUpdate(
                  "InsertHcsBIReservationDashboardJob",
                  () => serviceProvider.GetService<IApmaTaskSchedulerService>().SummaryScheduler("HcsBIReservationDashboard"),
                  configuration["ApmaHangfireJobSchedulerTime:HcsBIReservationDashboard"], TimeZoneInfo.Utc
                  );

            //recurringJobManager.AddOrUpdate(
            //    "HcsBISourceStatisticsHistory",
            //    () => serviceProvider.GetService<IHcsBISourceStatisticsService>().GetHcsBISourceStatistics(),
            //    configuration["* * * * *"], TimeZoneInfo.Utc
            //    );
        }
    }
}
