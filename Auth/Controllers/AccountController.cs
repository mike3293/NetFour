using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Auth.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;

        private UserManager<IdentityUser> _userManager;


        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost("login")]
        public async Task<SignInResult> Login([FromQuery] string email, [FromQuery] string pass)
        {
            var result = await _signInManager.PasswordSignInAsync(email, pass, true, false);

            return result;
        }

        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IdentityResult> ChangePassword([FromQuery] string currentPass, [FromQuery] string newPass)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(user, currentPass, newPass);

            return result;
        }
    }
}
