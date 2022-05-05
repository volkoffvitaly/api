using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IMapper mapper, 
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersInfo()
        {
            var users = _context.Users;
            var usersInfoDto = _mapper.ProjectTo<FullUserInfoDto>(users);

            return Ok(usersInfoDto);
        }

        [HttpGet("StudentMarks")]
        public async Task<IActionResult> GetMarksAsStudent(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            var sortedMarks = user.MarksAsStudent
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Semester);

            var marksDtos = _mapper.Map<List<MarkDto>>(sortedMarks);

            return Ok(marksDtos);
        }

        [HttpGet("AgentMarks")]
        public async Task<IActionResult> GetMarksAsAgent(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            var sortedMarks = user.MarksAsAgent
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Semester);

            var marksDtos = _mapper.Map<List<MarkDto>>(sortedMarks);

            return Ok(marksDtos);
        }

        [HttpGet("Marks")]
        public async Task<IActionResult> GetMarks(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            var sortedMarks = user.MarksAsAgent
                .Concat(user.MarksAsStudent)
                .Distinct()
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Semester);

            var marksDtos = _mapper.Map<List<MarkDto>>(sortedMarks);

            return Ok(marksDtos);
        }
                
        [HttpGet]
        [Route("UserInfo/{id}")]
        public async Task<IActionResult> GetUserInfo(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user == default)
                return NotFound();

            var userInfoDto = _mapper.Map<FullUserInfoDto>(user);

            return Ok(userInfoDto);
        }

        [HttpGet]
        [Route("UserInfo")]
        public async Task<IActionResult> GetUserInfo(string token)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == GetUsernameFromToken(token));
            var userRoles = await _userManager.GetRolesAsync(user);

            if (user == default)
                return NotFound();

            var userInfoDto = _mapper.Map<FullUserInfoDto>(user);
            userInfoDto.Roles = userRoles;

            return Ok(userInfoDto);
        }

        [HttpPut]
        [Route("UserInfo")]
        public async Task<IActionResult> UpdateUserInfo(string token, [FromBody] FullUserInfoEditDto fullUserInfoEditDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == GetUsernameFromToken(token));

            if (user == default)
                return NotFound();

            fullUserInfoEditDto.Email = fullUserInfoEditDto.Email?.Trim();
            fullUserInfoEditDto.FirstName = fullUserInfoEditDto.FirstName?.Trim();
            fullUserInfoEditDto.MiddleName = fullUserInfoEditDto.MiddleName?.Trim();
            fullUserInfoEditDto.LastName = fullUserInfoEditDto.LastName?.Trim();

            var userWithSameCredentials = await _userManager.Users.FirstOrDefaultAsync(y => y.Email == fullUserInfoEditDto.Email);

            if (userWithSameCredentials != default && userWithSameCredentials.Id != user.Id)
                return BadRequest("Пользователь с такой электронной почтой уже существует");

            user = _mapper.Map(fullUserInfoEditDto, user);

            _context.Update(user);
            await _context.SaveChangesAsync();

            var userInfoDto = _mapper.Map<FullUserInfoDto>(user);
            return Ok(userInfoDto);
        }

        private string GetUsernameFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            var username = claimsPrincipal.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Token generation/validation failed");

            return username;
        }
        
        [HttpGet]
        [Route("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.Select(_ => _.Name).ToListAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Route("{id}/Role")]
        public async Task<IActionResult> AddToRole(Guid id, string role)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = _context.Roles.Select(_ => _.Name).ToList();

            if(!roles.Contains(role))
            {
                throw new ArgumentException("Wrong role name");
            }

            try
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            catch
            {
                throw new Exception();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}/Role")]
        public async Task<IActionResult> RemoveFromRole(Guid id, string role)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = _context.Roles.Select(_ => _.Name).ToList();

            if (!roles.Contains(role))
            {
                throw new ArgumentException("Wrong role name");
            }

            try
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            catch
            {
                throw new Exception();
            }

            return Ok();
        }
    }
}