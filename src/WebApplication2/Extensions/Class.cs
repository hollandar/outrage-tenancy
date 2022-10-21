using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApplication2.Handlers;

namespace WebApplication2.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static RouteHandlerBuilder MapGetHandler<TRequest>(this IEndpointRouteBuilder builder, string route)
        {
            return builder.MapGet(route, async ([FromServices] IEnumerable<IHttpHandler> handlers, HttpContext context, [AsParameters] TRequest request) =>
            {
                var getHandlers = handlers.OfType<IHttpGetHandler<TRequest>>();
                if (getHandlers.Count() > 1)
                {
                    return Results.NotFound($"Multiple GET handlers for {nameof(TRequest)} exist.");
                }

                if (getHandlers.Count() == 0)
                {
                    return Results.NotFound($"No handlers for {nameof(TRequest)} exist.");
                }

                var getHandler = (IHttpGetHandler<TRequest>)getHandlers.First();

                return await getHandler.GetAsync(context, request);
            });
        }
    }
}
