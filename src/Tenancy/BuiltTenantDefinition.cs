using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Models;

namespace Outrage.Tenancy
{
    public class BuiltTenantDefinition: IBuiltTenantDefinition
    {
        private readonly TenantModel tenantDefinition;

        public BuiltTenantDefinition(TenantModel tenantDefinition)
        {
            this.tenantDefinition = tenantDefinition;
        }

        public TenantModel Definition { get { return tenantDefinition; } }
    }
}
