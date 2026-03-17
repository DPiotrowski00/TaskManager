namespace TaskManagerAPI.middleware
{
    public class LoggingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            Directory.CreateDirectory("C:\\Users\\damian.piotrowski\\Documents\\ProgramFiles\\tmAPI");
            
            string file = "C:\\Users\\damian.piotrowski\\Documents\\ProgramFiles\\tmAPI\\request_log.txt";

            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body);
            string body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            var headers = "";
            foreach (var header in context.Request.Headers)
            {
                headers += $"{header.Key}: {header.Value}\n";
            }

            var query = context.Request.QueryString.HasValue
            ? context.Request.QueryString.Value
            : "";

            var log =
            $"""
            ------------------------------
            Protocol: {context.Request.Protocol}
            Method: {context.Request.Method}
            Host: {context.Request.Host}
            Path: {context.Request.Path}
            Query: {query}

            Headers:
            {headers}

            Body:
            {body}
            ------------------------------
            """;

            File.AppendAllText(file, log);

            await _next(context);
        }
    }
}
