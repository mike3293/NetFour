using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Claims
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
        }


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            ClaimsIdentity claims = await base.GenerateClaimsAsync(user);

            claims.AddClaim(new Claim("AgeV2", $"{user.Age} year{(user.Age != 1 ? "s" : "")}"));

            return claims;
        }
    }
}
