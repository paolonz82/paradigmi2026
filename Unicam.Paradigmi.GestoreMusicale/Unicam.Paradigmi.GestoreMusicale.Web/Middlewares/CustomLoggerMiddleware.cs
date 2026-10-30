namespace Unicam.Paradigmi.GestoreMusicale.Web.Middlewares
{
    public class CustomLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/logger"))
            {
                await context.Response.WriteAsync("Scritto dal logger");
            }
            else
            {
                context.Items.Add("MyCustomItem", "NoLogger");
                Console.WriteLine("Stiamo entrando nel middleware CustomLogger");
                await _next(context);
            }
        }
    }
}
