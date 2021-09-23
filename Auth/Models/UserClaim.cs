using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class UserClaim
    {
        public AppUser User { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
