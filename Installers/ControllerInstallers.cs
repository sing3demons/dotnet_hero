using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_hero.Installers
{
    public class ControllerInstallers:IInstallers
    {
        public ControllerInstallers()
        {
        }

        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
        }
    }
}
