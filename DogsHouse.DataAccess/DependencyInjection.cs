using DogsHouse.DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DogsHouse.Application.Common.Interfaces;

namespace DogsHouse.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("DogsHouseSqlDB");

            services.AddDbContext<DogsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IDogsDbContext, DogsDbContext>();

            return services;
        }
    }
}
