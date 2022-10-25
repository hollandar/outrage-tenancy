using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Outrage.Tenancy;
using Outrage.Tenancy.Features;
using Outrage.Tenancy.Helpers;
using Outrage.Tenancy.Definition;
using WebApplication2.Data;

namespace WebApplication2.Tenancy
{
    public class TenancyBuilder : ITenantBuilder
    {
        private readonly ILogger<TenancyBuilder>? logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;

        public TenancyBuilder(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            logger = serviceProvider.GetService<ILogger<TenancyBuilder>>() ?? null;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }


        public async Task EstablishTenancyAsync(TenancyDefinition tenantDefinition)
        {
            using var databaseWrapper = serviceProvider.GetRequiredService<IDatabaseWrapper>();
            logger?.LogInformation($"Tenant {tenantDefinition.TenantId.AsString} established.");
            var tenantConnectionString = configuration.GetConnectionString("Tenant");
            var databaseName = $"d_" + tenantDefinition.TenantId.AsString;
            var username = $"u_{tenantDefinition.TenantId.AsString}";
            var password = Password.Generate(24);

            if (databaseWrapper.EstablishDatabase(databaseName, username, password))
            {
                var connectionStringFormat = configuration.GetConnectionString("Format");
                var connectionString = string.Format(connectionStringFormat, username, password, databaseName);

                tenantDefinition.Store.SetValue("ConnectionString", connectionString, Outrage.Tenancy.Data.ValueEncodingEnum.UTF8Crypt);
                await tenantDefinition.Store.SaveChangesAsync();
            }
        }

        public Task<IBuiltTenantDefinition> BuildTenancyAsync(TenancyDefinition tenantDefinition)
        {
            var builtDefinition = new BuiltTenantDefinition(tenantDefinition);
            logger?.LogInformation($"Tenant {tenantDefinition.TenantId.AsString} built.");

            // Establish the DbContext as a feature
            var dbContext = new DbContextTenancyFeature(tenantDefinition);
            tenantDefinition.Features.Set<IDbContextTenancyFeature<ReqsDbContext>>(dbContext);
            return Task.FromResult<IBuiltTenantDefinition>(builtDefinition);
        }


        public async Task MigrateTenancyAsync(IBuiltTenantDefinition tenantDefinition)
        {
            logger?.LogInformation($"Tenant {tenantDefinition.Definition.TenantId.AsString} migrated.");

            var builder = new DbContextOptionsBuilder();
            var dbContextFeature = tenantDefinition.Definition.Features.Get<IDbContextTenancyFeature<ReqsDbContext>>();
            var context = dbContextFeature.GetDbContext();
            context.Database.Migrate();

        }
    }
}
