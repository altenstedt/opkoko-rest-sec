using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ProductsService
{
    public class HeadersMiddleware
    {
        private readonly RequestDelegate next;
        
        public HeadersMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke(HttpContext context) {
            context.Response.OnStarting((state) => {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                // HSTS support will be included in ASP.NET .NET Core 2.1
                if (context.Request.IsHttps) {
                    const int maxAge = 31536000; // One year in seconds
                    context.Response.Headers.Add("Strict-Transport-Security", $"max-age={maxAge}; includeSubDomains");
                }

                // Please note that Kestrel will add this header back.
                // Update class Program to configure Kestrel.
                context.Response.Headers.Remove("Server");

                return Task.CompletedTask;
            }, null);

            await next(context);
        }
    }

    public static class HeadersMiddlewareExtensions  {
        public static IApplicationBuilder UseHeaders(this IApplicationBuilder app) {
            return app.UseMiddleware<HeadersMiddleware>();
        }
    }
}

