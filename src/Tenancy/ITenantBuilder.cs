using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Models;

namespace Outrage.Tenancy
{
    public interface ITenantBuilder
    {
        Task EstablishTenancyAsync(TenancyStore tenancyStore, TenantModel tenantDefinition);
        Task<IBuiltTenantDefinition> BuildTenancyAsync(TenancyStore tenancyStore, TenantModel tenantDefinition);
        Task MigrateTenancyAsync(TenancyStore tenancyStore, IBuiltTenantDefinition tenantDefinition);

    }
}
