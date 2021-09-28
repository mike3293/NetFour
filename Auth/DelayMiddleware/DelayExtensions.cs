using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.TimeoutMiddleware
{
    public static class DelayExtensions
    {
        public static IApplicationBuilder UseDelay(this IApplicationBuilder builder, int maxDelay = 120)
        {
            return builder.UseMiddleware<DelayMiddleware>(maxDelay);
        }
    }
}
