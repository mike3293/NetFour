using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Errors
{
    public class ValidationError : ApiError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }


        public ValidationError(string field, string message) : base(message)
        {
            Field = field != string.Empty ? field : null;
        }
    }
}
