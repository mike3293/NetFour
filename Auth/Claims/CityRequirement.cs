using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Claims
{
    public class CityRequirement : IAuthorizationRequirement
    {
        public string City { get; }


        public CityRequirement(string city)
        {
            City = city;
        }
    }
}
