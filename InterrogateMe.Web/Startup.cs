using InterrogateMe.Core.Data;
using InterrogateMe.Data;
using InterrogateMe.Utilities;
using InterrogateMe.Web.Hubs;
using InterrogateMe.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace InterrogateMe.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddEntityFrameworkNpgsql().AddDbContext<InterrogateDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("InterrogateDbContext")));

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRepository, Repository>();

            services.AddSingleton<InterrogateClient>();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(5);
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Question", "/{link}");
                    options.Conventions.AddPageRoute("/Result", "/r/{link}");
                    options.Conventions.AddPageRoute("/TermsOfService", "/tos");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            InitializeDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<InterrogateHub>("/interrogate");
                endpoints.MapRazorPages();
            });
            app.UseCookiePolicy();

            WebHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            ReCaptchaHelper.Configure(app.ApplicationServices.GetRequiredService<IConfiguration>());
        }

        private static void InitializeDatabase(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var interrogateDbContext = serviceScope.ServiceProvider.GetRequiredService<InterrogateDbContext>();
                interrogateDbContext.Database.Migrate();
            }
        }
    }
}
