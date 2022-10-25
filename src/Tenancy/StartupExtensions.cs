using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Data;
using Outrage.Tenancy.Options;
using Outrage.Tenancy.Providers;
using Microsoft.AspNetCore.Builder;

namespace Outrage.Tenancy
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

            return services;
        }
    public static IApplicationBuilder UseTenancy(this IApplicationBuilder application)
        {
            application.UseMiddleware<TenancyMiddleware>();
            return application;
        }
    }

}
