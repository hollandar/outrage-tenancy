namespace WebApplication2.Handlers
{

    public interface IHttpHandler
    {

    }

    public interface IHttpHandler<TRequest>: IHttpHandler
    {

    }

    public interface IHttpCrudHandler<TRequest>: IHttpGetHandler<TRequest>, IHttpPostHandler<TRequest>, IHttpDeleteHandler<TRequest>, IHttpPatchHandler<TRequest>
    {
    }

    public interface IHttpGetHandler<TRequest>: IHttpHandler<TRequest>
    {

        Task<IResult> GetAsync(HttpContext context, TRequest request);
    }

    public interface IHttpPostHandler<TRequest> : IHttpHandler<TRequest>
    {

        Task<IResult> PostAsync(HttpContext context, TRequest request);
    }

    public interface IHttpDeleteHandler<TRequest> : IHttpHandler<TRequest>
    {

        Task<IResult> DeleteAsync(HttpContext context, TRequest request);
    }

    public interface IHttpPutHandler<TRequest> : IHttpHandler<TRequest>
    {

        Task<IResult> PutAsync(HttpContext context, TRequest request);
    }

    public interface IHttpPatchHandler<TRequest> : IHttpHandler<TRequest>
    {

        Task<IResult> PatchAsync(HttpContext context, TRequest request);
    }
}
