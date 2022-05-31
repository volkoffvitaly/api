using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Helpers;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Auth;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        protected readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
        IConfiguration configuration)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<JwtInfoModel>> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
                return NotFound("Пользователь не существует");

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return NotFound("Неверный пароль");

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

            return Ok(new JwtInfoModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Username = model.Username?.Trim();
            model.Email = model.Email?.Trim();
            model.Password = model.Password?.Trim();
            model.ConfirmPassword = model.ConfirmPassword?.Trim();
            model.FirstName = model.FirstName?.Trim();
            model.MiddleName = model.MiddleName?.Trim();
            model.LastName = model.LastName?.Trim();

            var userWithSameCredentials = await _userManager.Users.FirstOrDefaultAsync(y => y.Email == model.Email);

            if (userWithSameCredentials != default)
                return BadRequest("Пользователь с такой электронной почтой уже существует");

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                BirthDate = model.DateOfBirth,
                Gender = model.Gender,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();

                var jwt = JwtHelper.GenerateJwt(user, _configuration);
                var refreshToken = JwtHelper.GenerateRefreshToken();
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                var newRefresh = new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Token = refreshToken,
                    TokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays)
                };
                _context.RefreshTokens.Add(newRefresh);
                await _context.SaveChangesAsync();

                var studentsRole = await _roleManager.FindByNameAsync(ApplicationRoles.Student);

                if (!await _userManager.IsInRoleAsync(user, studentsRole.Name))
                {
                    await _userManager.AddToRoleAsync(user, studentsRole.Name);
                }

                return Ok(new
                {
                    UserName = user.UserName,
                    UserId = user.Id,
                    AccessToken = jwt.Token,
                    RefreshToken = refreshToken,
                    Expiration = jwt.Expiration
                });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

