using Crvs.Core.Contracts;
using Crvs.Infrastructure.Logging;
using Crvs.Infrastructure.Persistence;
using Crvs.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Infrastructure
{
    public static class RegisterInfrastructureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
        {
            services.AddDbContext<CrvsDbContext>(
                options => 
                { 
                    options.UseNpgsql(config.GetConnectionString(""),o=> 
                    { 
                        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                    options.EnableDetailedErrors(env.IsDevelopment());
                    options.EnableSensitiveDataLogging(env.IsDevelopment());
                });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
        {
            services.AddPersistenceServices(config, env);
            services.AddScoped(typeof(IAppLogger<>),typeof(LoggingAdapter<>));
            return services;
        }
    }
}
