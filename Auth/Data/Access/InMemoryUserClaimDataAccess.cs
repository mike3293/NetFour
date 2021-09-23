using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Data.Access
{
    public class InMemoryUserClaimDataAccess
    {
        private List<UserClaim> _userClaims;


        public InMemoryUserClaimDataAccess()
        {
            _userClaims = new List<UserClaim>();
        }


        public bool AddClaims(AppUser user, IEnumerable<Claim> claims)
        {
            _userClaims.Add(new UserClaim() { User = user, Claims = claims });
            return true;
        }

        public IList<AppUser> GetUsers(Claim claim)
        {
            return _userClaims.Where(uc => uc.Claims.Any(c => c == claim)).Select(uc => uc.User).ToList();
        }

        public IList<Claim> GetClaims(string userId)
        {
            return _userClaims.Where(uc => uc.User.Id == userId).SelectMany(uc => uc.Claims).ToList();
        }
    }
}
