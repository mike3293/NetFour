using Auth.Data;
using Auth.Data.Access;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [ApiController]
    [Route("/api/admin")]
    public class AdminController : Controller
    {
        private IConfiguration _configuration;

        private RoleManager<IdentityRole> _roleManager;
        
        private InMemoryRoleDataAccess _roleAccess;

        private UserManager<AppUser> _userManager;

        private InMemoryUserDataAccess _userAccess;


        public AdminController(IConfiguration configuration, InMemoryRoleDataAccess roleAccess, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, InMemoryUserDataAccess userAccess)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _roleAccess = roleAccess;
            _userManager = userManager;
            _userAccess = userAccess;
        }


        [HttpGet("users")]
        [Authorize(Policy = "MainAdminOnly")]
        public IEnumerable<AppUser> GetUsers()
        {
            var users = _userAccess.GetAll();

            return users;
        }

        [HttpGet("roles")]
        [Authorize(Roles = "USER")]
        public IEnumerable<IdentityRole> GetRoles()
        {
            var roles = _roleAccess.GetAll();
            _userAccess.GetAll();

            return roles;
        }

        [HttpPost("createRoles")]
        public async Task CreateRoles()
        {
            string[] roleNames = { "ADMIN", "USER" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var commonUser = new AppUser
            {
                UserName = _configuration["User:UserName"],
                Email = _configuration["User:UserEmail"],
                EmailConfirmed = true
            };
            string userPass = _configuration["User:UserPassword"];

            await _userManager.CreateAsync(commonUser, userPass);
            await _userManager.AddToRoleAsync(commonUser, "User");

            var admin = new AppUser
            {
                Age = 20,
                UserName = _configuration["Admin:UserName"],
                Email = _configuration["Admin:UserEmail"],
                EmailConfirmed = true
            };
            string adminPass = _configuration["Admin:UserPassword"];

            await _userManager.CreateAsync(admin, adminPass);
            await _userManager.AddToRoleAsync(admin, "ADMIN");
            await _userManager.AddClaimAsync(admin, new Claim("IsMainAdmin", "true"));
        }

        [HttpPost("becomeAdmin")]
        [Authorize]
        public async Task<IdentityResult> BecomeAdmin()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            var s = await _userManager.IsInRoleAsync(user, "User");

            return result;
        }

        [HttpGet("claims")]
        [Authorize]
        public string GetClaims()
        {
            return HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Age").Value;
        }
    }
}
