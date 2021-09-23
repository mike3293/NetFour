using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class UserRole
    {
        public AppUser User { get; set; }

        public string Role { get; set; }
    }
}
