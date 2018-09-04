using InterrogateMe.Core.Data;
using InterrogateMe.Data;
using InterrogateMe.Utilities;
using InterrogateMe.Web.Hubs;
using InterrogateMe.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddEntityFrameworkNpgsql().AddDbContext<InterrogateDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("InterrogateDbContext")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRepository, Repository>();

            services.AddSingleton<InterrogateClient>();
                        
            services.AddSignalR(hubOptions => 
            {
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(5);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Question", "/{link}");
                    options.Conventions.AddPageRoute("/Result", "/r/{link}");
                    options.Conventions.AddPageRoute("/TermsOfService", "/tos");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseSignalR(route =>
            {
                route.MapHub<InterrogateHub>("/interrogate");
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    var path = ctx.File.PhysicalPath;
                    if (path.EndsWith(".css") || path.EndsWith(".js") || path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".png") || path.EndsWith(".svg"))
                    {
                        var maxAge = new TimeSpan(7, 0, 0, 0);
                        ctx.Context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                    }
                }
            });
            app.UseCookiePolicy();

            app.UseMvc();

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
