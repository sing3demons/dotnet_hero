using System;
using dotnet_hero.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_hero.Installers
{
    public class DbContextInstaller:IInstallers
    {
        public DbContextInstaller()
        {
        }

        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionSQLServer")));
        }
    }
}
