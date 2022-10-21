using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Models;

namespace Outrage.Tenancy
{
    sealed class DefaultTenantBuilder : ITenantBuilder
    {
        private readonly ILogger<DefaultTenantBuilder>? logger;

        public DefaultTenantBuilder(IServiceProvider serviceProvider)
        {
            this.logger = serviceProvider.GetService<ILogger<DefaultTenantBuilder>>() ?? null;
        }

        public Task<IBuiltTenantDefinition> BuildTenancyAsync(TenancyStore tenancyStore, TenantModel tenantDefinition)
        {
            var builtDefinition = new BuiltTenantDefinition(tenantDefinition);
            logger?.LogInformation($"Tenant {tenantDefinition.TenantId.AsString} built.");
            return Task.FromResult<IBuiltTenantDefinition>(builtDefinition);
        }

        public Task EstablishTenancyAsync(TenancyStore tenancyStore, TenantModel tenantDefinition)
        {
            logger?.LogInformation($"Tenant {tenantDefinition.TenantId.AsString} established.");
            return Task.CompletedTask;
        }

        public Task MigrateTenancyAsync(TenancyStore tenancyStore, IBuiltTenantDefinition tenantDefinition)
        {
            logger?.LogInformation($"Tenant {tenantDefinition.Definition.TenantId.AsString} migrated.");
            return Task.CompletedTask;
        }
    }
}
