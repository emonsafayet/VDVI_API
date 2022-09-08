using Framework.Core.Swagger;
using Framework.Core.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VDVI.Client.IoC;
using StartupBase = Framework.Core.Base.Startup.StartupBase;

namespace VDVI
{
    public class Startup : StartupBase
    {
        private static string ApiTitle => "VDVI Application";
        public Startup(IConfiguration configuration) : base(configuration, new UnityDependencyProvider(), ApiTitle)
        {

        }

        // public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<ApiVersionFilter>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
            });

            //services.AddControllers();

            //services



            //services.AddSwaggerGen();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "Implement Swagger UI",
            //        Description = "A simple example to Implement Swagger UI",
            //    });
            //});
            ////Hangfire
            //services.AddHangfire(config =>
            //           config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //           .UseSimpleAssemblyNameTypeSerializer()
            //           .UseDefaultTypeSerializer()
            //           .UseSqlServerStorage(Configuration.GetConnectionString("ApmaDb")
            //           ));
            //services.AddHangfireServer();


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                        //IConfiguration configuration,
                        //IBackgroundJobClient backgroundJobClient,
                        //IRecurringJobManager recurringJobManager,
                        //IServiceProvider serviceProvider,
                        IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {

            base.Configure(app, env, apiVersionDescriptionProvider);

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c =>
            //    {
            //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            //    });
            //}

            app.UseSwagger();

            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = string.Empty;

                    //INFO: Removes Schemas from Swagger API Documentation
                    options.DefaultModelsExpandDepth(-1);

                    options.InjectStylesheet("/swagger-ui/SwaggerDark.css");

                    // build a swagger endpoint for each discovered API version  
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });


            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(async endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    DashboardTitle = "Scheduled Jobs"
            //});

            //recurringJobManager.AddOrUpdate(
            //      "InsertReportManagementRoomAndLedgerJob",
            //      () => serviceProvider.GetService<IApmaTaskSchedulerService>().SummaryScheduler("HcsReportManagementSummary"),
            //      configuration["ApmaHangfireJobSchedulerTime:ReportManagementRoomAndLedgerSummary"], TimeZoneInfo.Utc
            //      );


        }
    }
}
