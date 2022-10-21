using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenancy.Models;

namespace Tenancy.Providers
{
    public sealed class HttpHeaderTenancyIdProvider : ITenancyIdProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpHeaderTenancyIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public TenantId GetTenantId()
        {
            var httpContext = this.httpContextAccessor.HttpContext;
            if (httpContext.Request.Headers.TryGetValue("X-TENANT-ID", out var tenantIds)) {
                if (tenantIds.Count > 1)
                    throw new TenantNotSpecifiedException($"Only a single X-TENANT-ID is required.");
                var tenantId = tenantIds[0];

                return new TenantId(tenantId);
            }

            throw new TenantNotSpecifiedException($"The header value X-TENANT-ID does not specify a tenant id.");
        }
    }
}
