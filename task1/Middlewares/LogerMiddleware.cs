

using System.Diagnostics;


namespace MyMiddleware.LogerMiddleware
{
    public class LogerMiddleware
    {
        private RequestDelegate next;
        private string  logger;


        public LogerMiddleware(RequestDelegate next, string  logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext c)
        {
            var sw = new Stopwatch();
            sw.Start();
            await next(c);
            WriteLogToFile($"{DateTime.Now} {c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms."
                + $" User: {c.User?.FindFirst("Id")?.Value ?? "unknown"}");     
        }  

        private void WriteLogToFile(string logMessage)
        {
            using (StreamWriter sw = File.AppendText(logger))
            {
                sw.WriteLine(logMessage);
            }
        }


    }


    public static class LogerMiddleExtensions
{
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder,string logFilePath)
    {
        return builder.UseMiddleware<LogerMiddleware>(logFilePath);
    }
}
}