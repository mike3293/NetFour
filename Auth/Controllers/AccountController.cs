using Auth.Models;
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
        private SignInManager<AppUser> _signInManager;

        private UserManager<AppUser> _userManager;


        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
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
