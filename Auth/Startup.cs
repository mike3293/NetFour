using Auth.Data;
using Auth.Data.Access;
using Auth.Data.Stores;
using Auth.Models;
using Auth.TimeoutMiddleware;
using Auth.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auth.Valiadators;

namespace Auth
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
            services.AddSingleton<InMemoryUserDataAccess>();
            services.AddSingleton<InMemoryRoleDataAccess>();
            services.AddSingleton<InMemoryUserRoleDataAccess>();
            services.AddSingleton<InMemoryUserClaimDataAccess>();

            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddUserStore<InMemoryUserStore>()
                .AddRoleStore<InMemoryRoleStore>()
                .AddUserValidator<UserNameValidator>()
                .AddUserValidator<UserEmailValidator>()
                .AddClaimsPrincipalFactory<AppClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            services.AddControllers();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MainAdminOnly", policy =>
                       policy.RequireRole("ADMIN").RequireClaim("IsMainAdmin", "true").AddRequirements(new CityRequirement("Minsk")));
            });

            services.AddScoped<IAuthorizationHandler, CityAuthorizationHandler>();

            services.Configure<SecurityStampValidatorOptions>(o =>
                o.ValidationInterval = TimeSpan.Zero);

            services.AddHealthChecks()
                //.AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"])
                .AddUrlGroup(new Uri("https://www.google.com/"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDelay(10);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(env.ContentRootPath, "MyStaticFiles")),
                RequestPath = "/staticFiles"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/");
                endpoints.MapControllers();
            });
        }
    }
}
