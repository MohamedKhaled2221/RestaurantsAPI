
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notfound)
            {

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
                logger.LogWarning(notfound.Message);

            }
            catch (ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("You don't have permission to access this resource.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong. Please try again later.");
            }
        }
    }
}
