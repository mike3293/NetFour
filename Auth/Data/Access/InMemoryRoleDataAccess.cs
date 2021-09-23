using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Data.Access
{
    public class InMemoryRoleDataAccess
    {
        private List<IdentityRole> _roles;


        public InMemoryRoleDataAccess()
        {
            _roles = new List<IdentityRole>();
        }


        public bool CreateRole(IdentityRole role)
        {
            _roles.Add(role);
            return true;
        }

        public bool RemoveRole(IdentityRole role)
        {
            return _roles.Remove(role);
        }

        public IList<IdentityRole> GetAll()
        {
            return _roles;
        }

        public IdentityRole GetById(string id)
        {
            return _roles.FirstOrDefault(u => u.Id == id);
        }

        public IdentityRole GetRoleByName(string name)
        {
            return _roles.FirstOrDefault(u => u.NormalizedName == name);
        }

        public bool Update(IdentityRole role)
        {
            return true;
        }
    }
}
