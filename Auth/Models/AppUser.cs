using Microsoft.AspNetCore.Identity;

namespace Auth.Models
{
    public class AppUser : IdentityUser
    {
        public int Age { get; set; }

        public string City { get; set; }
    }
}
