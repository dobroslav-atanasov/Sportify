namespace Sportify.Web
{
    using System;
    using System.Reflection;
    using AutoMapper;
    using AutoMapping;
    using Data;
    using Data.Models;
    using Data.ViewModels.Countries;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Middlewares.Extensions;
    using Services;
    using Services.Interfaces;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            // Configure DbContext
            services.AddDbContext<SportifyDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Services
            services.AddTransient<ICountriesService, CountriesService>();

            // Configure Identity
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<SportifyDbContext>();
            
            // Configure AutoMapper
            AutoMapperConfiguration.RegisterMappings(typeof(CountrySelectViewModel).Assembly);

            // Change password requirements
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
            });

            //services.AddDefaultIdentity<User>()
            //    .AddEntityFrameworkStores<SportifyDbContext>();

            // Configure AutoValidationAntiforgeryToken
            services.AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
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

            // Middleware add roles in database
            app.UseSeedRoles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}