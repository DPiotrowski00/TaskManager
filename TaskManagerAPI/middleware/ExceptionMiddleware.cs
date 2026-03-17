namespace TaskManagerAPI.middleware
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    error = "Internal server error",
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}