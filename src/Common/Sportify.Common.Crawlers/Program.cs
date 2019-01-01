namespace Sportify.Common.Crawlers
{
    using System;
    using System.IO;

    using Crawlers;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureService(serviceCollection);
            var serviceProvider = (IServiceProvider)serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                var countryCrawler = new CountryCrawler(serviceProvider);
                countryCrawler.Run();
            }
        }

        private static void ConfigureService(ServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddDbContext<SportifyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}