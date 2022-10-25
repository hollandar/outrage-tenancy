using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Definition;

namespace Outrage.Tenancy
{
    public interface ITenantBuilder
    {
        Task EstablishTenancyAsync(TenancyDefinition tenantDefinition);
        Task<IBuiltTenantDefinition> BuildTenancyAsync(TenancyDefinition tenantDefinition);
        Task MigrateTenancyAsync(IBuiltTenantDefinition tenantDefinition);

    }
}
