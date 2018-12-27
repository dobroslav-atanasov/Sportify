namespace Sportify.Tests
{
    using System;

    using AutoMapper;
    using Data;
    using Data.Models;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Interfaces;

    public class BaseServiceTests
    {
        protected BaseServiceTests()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
            services.AddOptions();
            services.AddDbContext<SportifyDbContext>(b => b.UseInMemoryDatabase("SportifyDbContext").UseInternalServiceProvider(efServiceProvider));

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<SportifyDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
            });

            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<ISportsService, SportsService>();
            services.AddTransient<IDisciplinesService, DisciplinesService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IVenuesService, VenuesService>();
            services.AddTransient<IOrganizationsService, OrganizationsService>();

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(
                new HttpContextAccessor()
                {
                    HttpContext = context,
                });

            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            this.Mapper = mapperConfig.CreateMapper();

            this.ServiceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; set; }

        public IMapper Mapper { get; set; }
    }
}