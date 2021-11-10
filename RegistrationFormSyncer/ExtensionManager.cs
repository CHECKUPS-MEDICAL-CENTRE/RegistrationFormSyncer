using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RegistrationFormSyncer.CheckDbContext;
using RegistrationFormSyncer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormSyncer
{
    public static class ExtensionManager
    {
        public static IServiceCollection AddModels(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<CheckupsDbContext>(options);
            services.AddScoped<IRegistrationDAL, RegistrationDAL>();
            return services;
        }
    }
}
