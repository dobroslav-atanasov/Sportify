using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportify.AutoMapper;
using Sportify.Data;
using Sportify.Data.Models;
using System;

namespace Sportify.Tests
{
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
