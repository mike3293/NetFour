using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Data.Access
{
    public class InMemoryUserDataAccess
    {
        private List<AppUser> _users;


        public InMemoryUserDataAccess()
        {
            _users = new List<AppUser>();
        }


        public bool CreateUser(AppUser user)
        {
            _users.Add(user);
            return true;
        }

        public bool RemoveUser(AppUser user)
        {
            return _users.Remove(user);
        }

        public IList<AppUser> GetAll()
        {
            return _users;
        }

        public AppUser GetUserById(string id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public AppUser GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.NormalizedEmail == email);
        }

        public AppUser GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.NormalizedUserName == username);
        }

        public string GetNormalizedUsername(AppUser user)
        {
            return user.NormalizedUserName;
        }

        public bool Update(AppUser user)
        {
            return true;
        }
    }
}
