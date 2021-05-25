using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace dotnet_hero.Installers
{
    public class SwaggerInstaller : IInstallers
    {


        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var Bearer = "Bearer";
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet_hero", Version = "v1" });
                //jwt
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = Bearer,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference                        {
                            Id="Bearer",Type=ReferenceType.SecurityScheme,
                        },
                        Scheme=Bearer,Name=Bearer,In=ParameterLocation.Header
                    },
                        new List<string>()
                    }
                });
            });
        }
    }
}
