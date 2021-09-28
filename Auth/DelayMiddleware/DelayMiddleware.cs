using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Auth.TimeoutMiddleware
{
    public class DelayMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly int _maxDelay;


        public DelayMiddleware(RequestDelegate next, int maxDelay)
        {
            _next = next;
            _maxDelay = maxDelay;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var timeout = context.Request.Query["timeout"];

            if(timeout.Count == 0)
            {
                await _next(context);
                return;
            }

            try
            {
                var parsedTimeout = Int32.Parse(timeout);

                if (parsedTimeout > _maxDelay)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync($"Max timeout is {_maxDelay} sec.");
                }
                else
                {
                    await Task.Delay(parsedTimeout * 1000);
                    await _next(context);
                }
            }
            catch
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid timeout value.");
            }
        }
    }
}
