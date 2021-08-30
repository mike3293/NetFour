using Auth.Data;
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

        private ApplicationDbContext _context;

        private RoleManager<IdentityRole> _roleManager;

        private UserManager<IdentityUser> _userManager;


        public AdminController(IConfiguration configuration, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpGet("users")]
        [Authorize(Policy = "MainAdminOnly")]
        public IEnumerable<IdentityUser> GetUsers()
        {
            var users = _context.Users.ToList();

            return users;
        }

        [HttpGet("roles")]
        [Authorize(Roles = "User")]
        public IEnumerable<IdentityRole> GetRoles()
        {
            var roles = _context.Roles.ToList();

            return roles;
        }

        [HttpPost("createRoles")]
        public async Task CreateRoles()
        {
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var commonUser = new IdentityUser
            {
                UserName = _configuration["User:UserName"],
                Email = _configuration["User:UserEmail"],
                EmailConfirmed = true
            };
            string userPass = _configuration["User:UserPassword"];

            await _userManager.CreateAsync(commonUser, userPass);
            await _userManager.AddToRoleAsync(commonUser, "User");

            var admin = new IdentityUser
            {
                UserName = _configuration["Admin:UserName"],
                Email = _configuration["Admin:UserEmail"],
                EmailConfirmed = true
            };
            string adminPass = _configuration["Admin:UserPassword"];

            await _userManager.CreateAsync(admin, adminPass);
            await _userManager.AddToRoleAsync(admin, "Admin");
            await _userManager.AddClaimAsync(admin, new Claim("IsMainAdmin", "true"));
        }

        [HttpPost("becomeAdmin")]
        [Authorize]
        public async Task<IdentityResult> BecomeAdmin()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            return result;
        }
    }
}
