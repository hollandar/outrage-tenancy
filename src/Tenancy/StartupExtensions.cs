using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenancy.Data;
using Tenancy.Options;
using Tenancy.Providers;

namespace Tenancy
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddTenancy(this IServiceCollection services, Action<DbContextOptionsBuilder> dbOptionsBuilder, Action<TenancyOptions> optionsBuilder = null)
        {
            var tenancyOptions = new TenancyOptions();
            if (optionsBuilder != null) optionsBuilder(tenancyOptions);
            services.TryAddSingleton<TenancyOptions>(tenancyOptions);

            services.AddDbContext<TenancyDbContext>(dbOptionsBuilder);

            services.TryAddScoped<ITenancyIdProvider, HttpHostTenancyIdProvider>();
            services.TryAddScoped<ITenancyService, TenancyService>();

            return services;
        }
    }
}
