using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Core
{
    public static class RegisterCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddMediatR((config) => { 
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}
