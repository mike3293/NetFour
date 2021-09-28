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
            IdentityResult result = IdentityResult.Failed();
            bool createResult = _dataAccess.CreateRole(role);

            if (createResult)
            {
                result = IdentityResult.Success;
            }

            return Task.FromResult(result);
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            IdentityResult result = IdentityResult.Failed();
            bool deleteResult = _dataAccess.RemoveRole(role);

            if (deleteResult)
            {
                result = IdentityResult.Success;
            }

            return Task.FromResult(result);
        }

        public void Dispose()
        {

        }

        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.GetById(roleId));
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.GetRoleByName(normalizedRoleName));
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            role.NormalizedName = roleName.ToUpper();

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
                IdentityResult result = IdentityResult.Failed();
                bool updateResult = _dataAccess.Update(role);

                if (updateResult)
                {
                    result = IdentityResult.Success;
                }

                return Task.FromResult(result);
        }
    }
}
