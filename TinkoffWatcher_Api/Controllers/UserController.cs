using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
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

        /// <summary>
        /// Получить оценки пользователя как студента
        /// </summary>
        /// <param name="token">Токен</param>
        [HttpGet("StudentMarks")]
        public async Task<IActionResult> GetMarksAsStudent(Guid id)
        {
            var user = await _context.Users
                    .Include(x => x.MarksAsStudent)
                    .SingleOrDefaultAsync(x => x.Id == id);

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
            var user = await _context.Users
                .Include(x => x.MarksAsAgent)
                .SingleOrDefaultAsync(x => x.Id == id);

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
            var user = await _context.Users
                .Include(x => x.MarksAsStudent)
                .Include(x => x.MarksAsAgent)
                .SingleOrDefaultAsync(x => x.Id == id);

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
                
        [HttpGet("UserInfo")]
        public async Task<IActionResult> GetUserInfo(string token)
        {
            var user = await _context.Users
                .Include(x => x.MarksAsStudent)
                .Include(x => x.MarksAsAgent)
                .SingleOrDefaultAsync(x => x.UserName == GetUsernameFromToken(token));

            if (user == null)
                return NotFound();

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
    }
}