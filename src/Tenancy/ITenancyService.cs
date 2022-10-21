using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenancy
{
    public interface ITenancyService
    {
        Task<TBuiltTenantDefinition> GetTenantAsync<TBuiltTenantDefinition>() where TBuiltTenantDefinition : IBuiltTenantDefinition;
        Task<IBuiltTenantDefinition> GetTenantAsync();
    }
}
