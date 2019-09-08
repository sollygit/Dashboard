using Dashboard.Extensions;
using Dashboard.Interfaces;
using Dashboard.Models;
using Dashboard.Models.JsonConverters;
using Dashboard.Models.Mappers;
using Dashboard.Models.Warnings;
using Dashboard.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dashboard
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
            services.AddDbContext<DashboardContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DashboardContext")));
            services.AddSingleton(new TimeZoneSettings
            {
                Id = Configuration.GetValue<string>("TimeZoneId")
            });
            services.AddSingleton(Configuration.GetSection("BranchFinder").Get<BranchFinderSettings>());
            services.AddSingleton(Configuration.GetSection("AdminApi").Get<AdminApiSettings>());
            services.AddSingleton<IWarning, StockOnHandWarning>();
            services.AddSingleton<IWarning, BackOrderWarning>();
            services.AddSingleton<IWarning, SubstitutionWarning>();
            services.AddSingleton<IWarning, OnHoldWarning>();
            services.AddSingleton<IWarning, ParkedWarning>();
            services.AddSingleton<IWarning, DeliveryEtaWarning>();
            services.AddSingleton<WarningTester>();
            services.AddSingleton<IMapper, GridRowMapper>();
            services.AddSingleton<IMapper, NationalOrderStatusMapper>();
            services.AddSingleton<DeliveryOrdersMapper>();
            services.AddScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<IDashboardUserRepository, DashboardUserRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddSingleton<ISourceRepository, InMemorySourceRepository>();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.Converters.Add(new PickerConverter());
                });
            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //    })
            //    .AddCookie()
            //    .AddOpenIdConnect(options =>
            //    {
            //        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.ClientId = Configuration.GetValue<string>("ADFS:ClientId");
            //        options.Authority = Configuration.GetValue<string>("ADFS:Authority");
            //        options.CallbackPath = new PathString("/signin-oidc");
            //        options.Scope.Add("email");
            //        options.GetClaimsFromUserInfoEndpoint = true;
            //        options.Events.OnTicketReceived = async context =>
            //        {
            //            await context.HttpContext.RequestServices.GetRequiredService<IDashboardUserRepository>().GetAdditionalClaims(context.Principal.Identity as ClaimsIdentity);
            //        };
            //    });

            // Register the Swagger services
            services.AddSwaggerDocument();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
