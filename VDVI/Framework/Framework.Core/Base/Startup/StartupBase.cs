using Framework.Core.Extensions;
using Framework.Core.IoC;
using Framework.Core.Jwt;
using Framework.Core.Logger;
using Framework.Core.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Unity;
using Unity.Lifetime;
using Log = Serilog.Log;

namespace Framework.Core.Base.Startup
{
    public abstract class StartupBase
    {
        private readonly IDependencyProvider _dependencyProvider;
        private readonly string _apiTitle;

        protected IConfiguration Configuration { get; }

        // TODO: Get this from somewhere secure
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(SecretKey.GetBytes());

        protected StartupBase(IConfiguration configuration, IDependencyProvider dependencyProvider, string apiTitle)
        {
            _dependencyProvider = dependencyProvider;
            _apiTitle = apiTitle;

            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add CORS policy
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        policyBuilder =>
            //        {
            //            policyBuilder
            //                .SetIsOriginAllowed(host => true)
            //                .AllowAnyOrigin()
            //                .AllowAnyMethod()
            //                .AllowAnyHeader();
            //        });
            //});

            // jwt wire up
            // Get options from app settings
            //var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            //services.Configure<AppConfigs>(Configuration.GetSection(nameof(AppConfigs)));

            // Configure JwtIssuerOptions
            //services.Configure<JwtIssuerOptions>(options =>
            //{
            //    options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            //    options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            //    options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            //});

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Constants.Jwt.ClaimIdentifiers.Rol, Constants.Constants.Jwt.Claims.ApiAccess));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddCookie(cookie =>
            {
                cookie.Cookie.HttpOnly = false; //flag that says cookie only avail to servers,  browser cannot access with JS.
                cookie.Cookie.SecurePolicy = CookieSecurePolicy.None; // cookie always uses HTTPS, recommendation: always in prod, none in dev.
                cookie.ExpireTimeSpan = TimeSpan.FromHours(2);

            }).AddJwtBearer(configureOptions =>
            {
                //configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                //configureOptions.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateIssuer = true,
                //    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                //    ValidateAudience = true,
                //    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = _signingKey,

                //    //RequireExpirationTime = true,
                //    //ValidateLifetime = true,

                //    // set clockSkew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                //    ClockSkew = TimeSpan.Zero
                //};

                configureOptions.SaveToken = true;

                //configureOptions.Events = new JwtBearerEvents
                //{
                //    OnAuthenticationFailed = async context =>
                //    {
                //        if (context.Exception.GetType() == typeof(SecurityTokenInvalidSignatureException)
                //            || context.Exception.GetType() == typeof(SecurityTokenInvalidLifetimeException)
                //            || context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                //        {
                //            context.Response.Headers.Add("Token-Expired", "true");
                //            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                //            await context.Response.WriteAsync(PrometheusResponse.Failure("Token has expired. User need to logout and login again.").Error);
                //        }
                //    },
                //    OnChallenge = async context =>
                //    {
                //        context.HandleResponse();
                //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //        context.Response.ContentType = "application/json";

                //        await context.Response.WriteAsync(PrometheusResponse.Failure("You are not Authorized. User need to logout and login again.").Error);
                //    },
                //    OnForbidden = async context =>
                //    {
                //        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                //        context.Response.ContentType = "application/json";

                //        await context.Response.WriteAsync(PrometheusResponse.Failure("You are not authorized to access this resource. User need to logout and login again.").Error);
                //    },
                //};
            });

            // add identity
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers().AddNewtonsoftJson();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.File($@"{Directory.GetCurrentDirectory()}\log\log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            services.AddSingleton(Log.Logger);

            services.Configure<GzipCompressionProviderOptions>(options =>
             {
                 options.Level = CompressionLevel.Optimal;
             });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddMemoryCache();

            services.AddDistributedMemoryCache();

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
                options.SchemaName = "dbo";
                options.TableName = "Cache";
            });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            //services.AddApiVersioning(o =>
            //{
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.ReportApiVersions = true;
            //    o.DefaultApiVersion = ApiVersion.Default;
            //    o.ApiVersionReader = new HeaderApiVersionReader(Constants.Constants.HttpRequestHeader.ApiVersion.DefaultHeader);
            //});

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service  
                // note: the specified format code will format the version as "'v'major[.minor][-status]"  
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat  
                // can also be used to control the format of the API version in route templates  
                options.SubstituteApiVersionInUrl = true;
            });


            services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseDefaultFiles()
               .UseStaticFiles();

            // Use the CORS policy
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            // global error handler
            //app.UseMiddleware<ApplicationAccessMiddleware>();

            //app.UseMiddleware<ErrorHandlerMiddleware>();

            // Enable compression
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // ReSharper disable once UnusedMember.Global
        // Called on run time
        public void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterFactory<IConfiguration>(m =>
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppSettings.json", false, true)
                    .AddJsonFile($"AppSettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                    .AddEnvironmentVariables()
                    .Build();

                return configuration;
            }, new SingletonLifetimeManager());

            container.RegisterType<IProLogger, ProLogger>();

            _dependencyProvider.RegisterDependencies(container);
        }
    }
}