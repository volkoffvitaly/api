using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Responses;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(90),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new ApplicationUser { UserName = model.Username, Email = model.Email, };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

