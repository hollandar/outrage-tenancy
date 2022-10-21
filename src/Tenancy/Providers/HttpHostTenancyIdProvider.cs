using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outrage.Tenancy.Models;
using Outrage.Tenancy.Options;

namespace Outrage.Tenancy.Providers
{
    public sealed class HttpHostTenancyIdProvider : ITenancyIdProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpHostTenancyOptions httpHostTenancyOptions;

        public HttpHostTenancyIdProvider(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.httpHostTenancyOptions = serviceProvider.GetService<HttpHostTenancyOptions>() ?? new();
        }

        public TenantId GetTenantId()
        {
            var httpContext = this.httpContextAccessor.HttpContext;
            if (!httpContext.Request.Host.HasValue)
            {
                throw new TenantNotSpecifiedException("Host was not available on the request");
            }

            var tenantId = httpContext.Request.Host.Host;
            if (httpContext.Request.Host.Port.HasValue && !this.httpHostTenancyOptions.IgnoredPorts.Contains(httpContext.Request.Host.Port.Value))
            {
                tenantId = $"{tenantId}--{httpContext.Request.Host.Port.Value}";
            }

            return new TenantId(tenantId.Replace('.','-'));
        }
    }
}
