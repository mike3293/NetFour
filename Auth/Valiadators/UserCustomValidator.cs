using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Valiadators
{
    public class UserCustomValidator : UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var defaultValidationResult = await base.ValidateAsync(manager, user);

            if (!defaultValidationResult.Succeeded)
            {
                return defaultValidationResult;
            }

            List<IdentityError> errors = new List<IdentityError>();

            if (!user.Email.ToLower().EndsWith("@test.ru"))
            {
                errors.Add(new IdentityError
                {
                    Code = "InvalidEmailDomain",
                    Description = "Email domain is outside the scope of application (test.ru)"
                });
            }

            return errors.Count == 0
                ? IdentityResult.Success
                : IdentityResult.Failed(errors.ToArray());
        }
    }
}
