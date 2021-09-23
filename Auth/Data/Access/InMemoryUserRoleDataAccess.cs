using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Data.Access
{
    public class InMemoryUserRoleDataAccess
    {
        private List<UserRole> _userRoles;


        public InMemoryUserRoleDataAccess()
        {
            _userRoles = new List<UserRole>();
        }


        public bool AddToRole(AppUser user, string role)
        {
            _userRoles.Add(new UserRole() { User = user, Role = role });
            return true;
        }

        public bool RemoveUserRole(string userId, string role)
        {
            return _userRoles.RemoveAll(ur => ur.User.Id == userId && ur.Role == role) > 0;
        }

        public IList<string> GetAll()
        {
            return _userRoles.Select(ur => ur.Role).ToList();
        }

        public IList<AppUser> GetInRole(string role)
        {
            return _userRoles.Where(ur => ur.Role == role).Select(ur => ur.User).ToList();
        }
    }
}
