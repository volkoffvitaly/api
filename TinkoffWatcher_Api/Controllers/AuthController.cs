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
        private readonly IConfiguration _configuration;

        public AuthController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<JwtInfoModel>> Login([FromBody] LoginModel model)
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

            return Ok(new JwtInfoModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Username = model.Username?.Trim();
            model.Email = model.Email?.Trim();
            model.Password = model.Password?.Trim();
            model.ConfirmPassword = model.ConfirmPassword?.Trim();
            model.FCs = model.FCs?.Trim();

            var userWithSameCredentials = _userManager.Users.FirstOrDefaultAsync(y => y.Email == model.Email);

            if (userWithSameCredentials != default)
                return BadRequest("Пользователь с такой электронной почтой уже существует");

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FCs, // Мне не нравится ФИО одним полем во фронте, потенциально может доставить неудобства
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
                var userId = Guid.Parse(user.Id);

                var newRefresh = new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Token = refreshToken,
                    TokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays)
                };
                _context.RefreshTokens.Add(newRefresh);
                await _context.SaveChangesAsync();

                return Ok();
                //return Json(new // чет не работает
                //{
                //    UserName = user.UserName,
                //    UserId = user.Id,
                //    AccessToken = jwt.Token,
                //    RefreshToken = refreshToken,
                //    Expiration = jwt.Expiration
                //});
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
}

