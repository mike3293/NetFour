using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.TimeoutMiddleware
{
    public static class TimeoutExtensions
    {
        public static IApplicationBuilder UseTimeout(this IApplicationBuilder builder, int maxTimeout = 120)
        {
            return builder.UseMiddleware<TimeoutMiddleware>(maxTimeout);
        }
    }
}
