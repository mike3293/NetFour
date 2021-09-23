using Auth.Data.Access;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Data.Stores
{
    public class InMemoryRoleStore : IRoleStore<IdentityRole>
    {
        private InMemoryRoleDataAccess _dataAccess;


        public InMemoryRoleStore(InMemoryRoleDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }


        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                IdentityResult result = IdentityResult.Failed();
                bool createResult = _dataAccess.CreateRole(role);

                if (createResult)
                {
                    result = IdentityResult.Success;
                }

                return result;
            });
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                IdentityResult result = IdentityResult.Failed();
                bool deleteResult = _dataAccess.RemoveRole(role);

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

        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _dataAccess.GetById(roleId);
            });
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _dataAccess.GetRoleByName(normalizedRoleName);
            });
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return role.NormalizedName;
            });
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return role.Id;
            });
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() => role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                role.NormalizedName = normalizedName;
            });
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                role.Name = roleName;
                role.NormalizedName = roleName.ToUpper();
            });
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                IdentityResult result = IdentityResult.Failed();
                bool updateResult = _dataAccess.Update(role);

                if (updateResult)
                {
                    result = IdentityResult.Success;
                }

                return result;
            });
        }
    }
}
