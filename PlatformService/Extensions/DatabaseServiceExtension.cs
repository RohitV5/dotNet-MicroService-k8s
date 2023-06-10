


using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

namespace PlatformService.Extensions
{

    public static class DatabaseServiceExtension
    {

        // Running migration will execute this file to check what database is getting used
        // Both the class and methos should be static and the function should return IServiceCollection
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                Console.WriteLine("Using a sqlserver");
                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                // Migration for inmemory database fails because it is available when application runs
                // As a hack comment out inMem block and remove isProduction check to generate migrations
                Console.WriteLine("Using a mem db");
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            }

            return services;
        }
    }

}