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


        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }


        [HttpPost("login")]
        public async Task<SignInResult> Login([FromQuery] string email, [FromQuery] string pass)
        {
            var result = await _signInManager.PasswordSignInAsync(email, pass, true, false);

            return result;
        }
    }
}
