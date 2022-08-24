using Hangfire.MemoryStorage;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.OpenApi.Models;
using SOAPAppCore.Interfaces;
using SOAPAppCore.Interfaces.Apma;
using SOAPAppCore.Services;
using SOAPAppCore.Services.Apma;
using System;
using VDVI.Common;
using VDVI.DB.IServices;
using VDVI.DB.Services;
using VDVI.DB.Repository;
using VDVI.DB.IRepository;
using VDVI.Services.IServices;
using VDVI.Services.Services;

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
            services.AddScoped<IReportManagementSummariesService,ReportManagementSummariesService>();
            services.AddScoped<IReportManagementDataInsertionService, ReportManagementDataInsertionService>();
            services.AddScoped<IApmaTaskSchedulerService, ApmaTaskSchedulerService>();

            //repositories
            services.AddScoped<IManagementRoomSummaryRepository, ManagementRoomSummaryRepository>();
         

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

            //dependency resolve:
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IApmaService, ApmaService>();
            services.AddTransient<IReportManagementSummary, ReportManagementSummary>();
            services.AddTransient<IManagementRoomSummaryRepository, ManagementRoomSummaryRepository>();
            services.AddTransient<ITaskSchedulerRepository, TaskSchedulerRepository>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
                        ,IConfiguration configuration,
                        IBackgroundJobClient backgroundJobClient,
                        IRecurringJobManager recurringJobManager,
                        IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
                });
            }

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
                  "InsertReportManagementRoomAndLedgerJob",
                  () => serviceProvider.GetService<IReportManagementSummariesService>().InsertReportManagenetRoomAndLedgerData(),
                  configuration["ApmaHangfire:ReportManagementRoomAndLedgerSummary"], TimeZoneInfo.Local
                  );
            //recurringJobManager.AddOrUpdate(
            //    "NotifyIncidentOwnerJob",
            //    () => serviceProvider.GetService<ISystemNotificationBackgroundService>().NotifyOwnerEmailAcknowledgementAsync(),
            //    configuration["* * * *"], TimeZoneInfo.Local
            //    );
        }
    }
}
