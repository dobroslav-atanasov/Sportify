﻿namespace Sportify.Web
{
    using System;

    using AutoMapper;
    using Data;
    using Data.Models;
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
                    options.UseLazyLoadingProxies()
                        .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Services
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<ISportsService, SportsService>();
            services.AddTransient<IDisciplinesService, DisciplinesService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IVenuesService, VenuesService>();
            services.AddTransient<IOrganizationsService, OrganizationsService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IParticipantsService, ParticipantsService>();

            // Configure Identity
            services.AddIdentity<User, IdentityRole>()
                //.AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<SportifyDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Identity/Users/SignIn");
                options.AccessDeniedPath = new PathString("/Home/Access");
            });

            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

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

            // Configure AutoValidateAntiforgeryToken
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
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}