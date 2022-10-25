using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Definition;

namespace Outrage.Tenancy
{
    public class BuiltTenantDefinition: IBuiltTenantDefinition
    {
        private readonly TenancyDefinition tenantDefinition;

        public BuiltTenantDefinition(TenancyDefinition tenantDefinition)
        {
            this.tenantDefinition = tenantDefinition;
        }

        public TenancyDefinition Definition { get { return tenantDefinition; } }

        public void Dispose()
        {
            this.tenantDefinition?.Dispose();
        }
    }
}
