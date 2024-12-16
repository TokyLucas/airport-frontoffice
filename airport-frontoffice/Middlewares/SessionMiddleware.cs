using airport_frontoffice.Helpers;
using airport_frontoffice.Models;

namespace airport_frontoffice.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip session validation for the login page or static resources
            var path = context.Request.Path.ToString().ToLower();
            if (
                path.EndsWith("/login") || path.EndsWith("/signin") || path.StartsWith("client")
                || path.StartsWith("/css") || path.StartsWith("/js") || path.StartsWith("/img") || path.StartsWith("/lib"))
            {
                await _next(context);
                return;
            }

            // Check if the session key exists
            if (context.Session.GetObjectFromJson<Client>("Client") == null)
            {
                context.Response.Redirect("/Client/Login");
                return;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
