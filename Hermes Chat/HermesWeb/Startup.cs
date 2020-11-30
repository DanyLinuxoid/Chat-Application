using FluentValidation.AspNetCore;
using HermesShared.Configuration;
using HermesWeb.Filters;
using HermesWeb.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HermesWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider) 
        {
            Configuration = configuration;
            DependencyInjector.SetProvider(serviceProvider);
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Cookie/Session/Cache section
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.SlidingExpiration = true;
                    options.CookieName = "HermesCookie";
                    options.LogoutPath = "/Home/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(ConfigurationManager.DefaultUserSessionExpirationTimeInMinutes);
                });
            services.AddSession(options =>
            {
                // 20 min session is default, but we will set 30.
                options.IdleTimeout = TimeSpan.FromMinutes(ConfigurationManager.DefaultUserSessionExpirationTimeInMinutes);
            });

            // Dependency section
            DependencyInjector.RegisterClasses(services);

            // Chat
            services.AddSignalR();

            // MVC
            services.AddMvc(options => 
            {
                options.Filters.Add(new AuthorizeFilter());
                options.Filters.Add<SessionStateFilterGlobalAttribute>();
            })
            .AddFluentValidation()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            DependencyInjector.SetProvider(app.ApplicationServices);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Authentication
            app.UseAuthentication();
            // Cookies
            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Strict,
            });
            // Session
            app.UseSession();
            // SignalR for one-to-many relationship in chat
            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/Chat/Index");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}