using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy
{
    public class TenancyMiddleware
    {
        private readonly RequestDelegate next;

        public TenancyMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            var tenancyService = new TenancyBuilder(context.RequestServices);
            using var tenancyDefinition = await tenancyService.GetTenantAsync();
            context.Features.Set<IBuiltTenantDefinition>(tenancyDefinition);

            this.next?.Invoke(context);

        }
    }
}
