using Auth.Data.Access;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Claims
{
    public class CityAuthorizationHandler : AuthorizationHandler<CityRequirement>
    {
        private UserManager<AppUser> _userManager;


        public CityAuthorizationHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CityRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);

            if (user.City == requirement.City)
            {
                context.Succeed(requirement);
            }
        }
    }
}
