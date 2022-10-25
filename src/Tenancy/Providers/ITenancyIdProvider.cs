using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Definition;

namespace Outrage.Tenancy.Providers
{
    public interface ITenancyIdProvider
    {
        TenantId GetTenantId();
    }
}
