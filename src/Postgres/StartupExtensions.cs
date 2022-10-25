using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Outrage.Tenancy.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Postgres
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddTenancyPostgres(this IServiceCollection services, IConfiguration configuration, string connectionStringName, Action<TenancyOptions> optionsBuilder = null)
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);
            services.AddScoped<IDatabaseWrapper, PostgresDatabaseWrapper>((sp) => new PostgresDatabaseWrapper(connectionString));
            return services.AddTenancy(dbOptions => dbOptions.UseNpgsql(connectionString, o => o.MigrationsAssembly("Outrage.Tenancy.Postgres")), optionsBuilder);
        }
    }
}
