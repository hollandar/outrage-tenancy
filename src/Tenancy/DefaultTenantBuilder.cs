using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Definition;

namespace Outrage.Tenancy
{
    sealed class DefaultTenantBuilder : ITenantBuilder
    {
        private readonly ILogger<DefaultTenantBuilder>? logger;

        public DefaultTenantBuilder(IServiceProvider serviceProvider)
        {
            this.logger = serviceProvider.GetService<ILogger<DefaultTenantBuilder>>() ?? null;
        }

        public Task<IBuiltTenantDefinition> BuildTenancyAsync(TenancyDefinition tenantDefinition)
        {
            var builtDefinition = new BuiltTenantDefinition(tenantDefinition);
            logger?.LogInformation($"Tenant {tenantDefinition.TenantId.AsString} built.");
            return Task.FromResult<IBuiltTenantDefinition>(builtDefinition);
        }

        public Task EstablishTenancyAsync(TenancyDefinition tenantDefinition)
        {
            logger?.LogInformation($"Tenant {tenantDefinition.TenantId.AsString} established.");
            return Task.CompletedTask;
        }

        public Task MigrateTenancyAsync(IBuiltTenantDefinition tenantDefinition)
        {
            logger?.LogInformation($"Tenant {tenantDefinition.Definition.TenantId.AsString} migrated.");
            return Task.CompletedTask;
        }
    }
}
