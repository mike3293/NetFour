using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Auth.TimeoutMiddleware
{
    public class TimeoutMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly int _maxTimeout;


        public TimeoutMiddleware(RequestDelegate next, int maxTimeout)
        {
            _next = next;
            _maxTimeout = maxTimeout;
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

                if (parsedTimeout > _maxTimeout)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync($"Max timeout is {_maxTimeout} sec.");
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
