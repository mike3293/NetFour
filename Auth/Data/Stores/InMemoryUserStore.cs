using Auth.Data.Access;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Data.Stores
{
    public class InMemoryUserStore : IUserPasswordStore<AppUser>, IUserEmailStore<AppUser>, IUserRoleStore<AppUser>, IUserClaimStore<AppUser>
    {
        private InMemoryUserDataAccess _userDataAccess;

        private InMemoryUserRoleDataAccess _userRoleDataAccess;

        private InMemoryUserClaimDataAccess _userClaimDataAccess;


        public InMemoryUserStore(InMemoryUserDataAccess userDataAccess, InMemoryUserRoleDataAccess userRoleDataAccess, InMemoryUserClaimDataAccess userClaimDataAccess)
        {
            _userDataAccess = userDataAccess;
            _userRoleDataAccess = userRoleDataAccess;
            _userClaimDataAccess = userClaimDataAccess;
        }


        public Task AddClaimsAsync(AppUser user, IEnumerable<Claim> claims, CancellationToken _)
        {
            return Task.Run(() =>
            {
                _userClaimDataAccess.AddClaims(user, claims);
            });
        }

        public Task AddToRoleAsync(AppUser user, string roleName, CancellationToken _)
        {
            return Task.Run(() =>
            {
                _userRoleDataAccess.AddToRole(user, roleName);
            });
        }

        public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() =>
            {
                IdentityResult result = IdentityResult.Failed();
                bool createResult = _userDataAccess.CreateUser(user);

                if (createResult)
                {
                    result = IdentityResult.Success;
                }

                return result;
            });
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() =>
            {
                IdentityResult result = IdentityResult.Failed();
                bool deleteResult = _userDataAccess.RemoveUser(user);

                if (deleteResult)
                {
                    result = IdentityResult.Success;
                }

                return result;
            });
        }

        public void Dispose()
        {

        }

        public Task<AppUser> FindByEmailAsync(string normalizedEmail, CancellationToken _)
        {
            return Task.Run(() =>
            {
                return _userDataAccess.GetByEmail(normalizedEmail);
            });
        }

        public Task<AppUser> FindByIdAsync(string userId, CancellationToken _)
        {
            return Task.Run(() =>
            {
                return _userDataAccess.GetUserById(userId);
            });
        }

        public Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken _)
        {
            return Task.Run(() =>
            {
                return _userDataAccess.GetUserByUsername(normalizedUserName);
            });
        }

        public Task<IList<Claim>> GetClaimsAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() =>
            {
                return _userClaimDataAccess.GetClaims(user.Id);
            });
        }

        public Task<string> GetEmailAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() =>
            {
                return _userRoleDataAccess.GetAll();
            });
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => user.Id);
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => user.UserName);
        }

        public Task<IList<AppUser>> GetUsersForClaimAsync(Claim claim, CancellationToken _)
        {
            return Task.Run(() =>
            {
                return _userClaimDataAccess.GetUsers(claim);
            });
        }

        public Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken _)
        {
            return Task.Run(() =>
            {
                return _userRoleDataAccess.GetInRole(roleName);
            });
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() => true);
        }

        public Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken _)
        {
            return Task.Run(() =>
            {
                var users = _userRoleDataAccess.GetInRole(roleName);

                return users.Any(u => u.Id == user.Id);
            });
        }

        public Task RemoveClaimsAsync(AppUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken _)
        {
            return Task.Run(() =>
            {
                _userRoleDataAccess.RemoveUserRole(user.Id, roleName);
            });
        }

        public Task ReplaceClaimAsync(AppUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailAsync(AppUser user, string email, CancellationToken _)
        {
            return Task.Run(() =>
            {
                user.Email = email;
            });
        }

        public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken _)
        {
            return Task.Run(() =>
            {
                user.EmailConfirmed = confirmed;
            });
        }

        public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail, CancellationToken _)
        {
            return Task.Run(() =>
            {
                user.NormalizedEmail = normalizedEmail;
            });
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken _)
        {
            return Task.Run(() =>
            {
                user.NormalizedUserName = normalizedName;
            });
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken _)
        {
            return Task.Run(() => { user.PasswordHash = passwordHash; });
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken _)
        {
            return Task.Run(() =>
            {
                user.UserName = userName;
                user.NormalizedUserName = userName.ToUpper();
            });
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken _)
        {
            return Task.Run(() =>
            {
                IdentityResult result = IdentityResult.Failed();
                bool updateResult = _userDataAccess.Update(user);

                if (updateResult)
                {
                    result = IdentityResult.Success;
                }

                return result;
            });
        }
    }
}
