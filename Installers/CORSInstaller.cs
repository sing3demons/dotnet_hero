using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_hero.Installers
{
    public class CORSInstaller : IInstallers
    {
        string[] domainName = {"http://www.sing3demons.com", "https://www.sing3demons.com" };

        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowSpecificOrigins",builder => {
                    builder.WithOrigins(domainName).AllowAnyHeader().AllowAnyHeader();
                });

                opt.AddPolicy("AllowAllOrigins", builder => {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
                });
            });
        }
    }
}
