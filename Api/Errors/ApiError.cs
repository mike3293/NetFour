using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Errors
{
    public class ApiError
    {
        public string Message { get; }


        public ApiError(string message)
        {
            Message = message;
        }
    }
}
