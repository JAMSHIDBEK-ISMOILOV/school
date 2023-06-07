using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Abstractions;
using School.Infrastructure.Persistence;

namespace School.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                
            });

            return services;
        }
    }
}

