using Auth.Data.Access;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Transformation
{
    internal class AddAgeClaimTransformation : IClaimsTransformation
    {
        private InMemoryUserDataAccess _userDataAccess;

        public AddAgeClaimTransformation(InMemoryUserDataAccess userService)
        {
            _userDataAccess = userService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;

            var nameId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier ||
                                                              c.Type == ClaimTypes.Name);
            if (nameId == null)
            {
                return principal;   
            }

            var user = _userDataAccess.GetUserById(nameId.Value);
            if (user == null)
            {
                return principal;
            }

            // Add role claims to cloned identity
            var claim = new Claim("Age", user.Age.ToString());
            newIdentity.AddClaim(claim);

            return clone;
        }
    }
}
