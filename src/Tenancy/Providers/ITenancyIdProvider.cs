using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenancy.Models;

namespace Tenancy.Providers
{
    public interface ITenancyIdProvider
    {
        TenantId GetTenantId();
    }
}
