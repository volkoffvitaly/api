using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Filters;
using TinkoffWatcher_Api.Models;
using Xceed.Words.NET;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IMapper mapper, 
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersInfo([FromQuery] UserFilter userFilter)
        {
            var users = string.IsNullOrWhiteSpace(userFilter.Role) ? 
                _userManager.Users.ToList() : 
                await _userManager.GetUsersInRoleAsync(userFilter.Role);

            var usersInfoDto = _mapper.Map<List<FullUserInfoDto>>(users);

            return Ok(usersInfoDto);
        }

        [HttpGet("StudentMarks")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
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
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
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
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.Select(_ => _.Name).ToListAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Route("{id}/Role")]
        [Authorize(Roles = ApplicationRoles.Administrators)]
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
        [Authorize(Roles = ApplicationRoles.Administrators)]
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

        [HttpGet]
        [Route("{id}/PracticeDiary")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.Student + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> GetPracticeDiary(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return NotFound($"User with id: {id} isn't exist");

            if (user.Company == null)
                return BadRequest($"User with id: {id} isn't intern");

            var templateFilePath = Path.Combine(
                _webHostEnvironment.WebRootPath, 
                _configuration["WwwrootPathKeys:Files"], 
                _configuration["WwwrootPathKeys:PracticeDiaryTemplate"]
            );

            var copyFilePath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                _configuration["WwwrootPathKeys:Files"],
                Guid.NewGuid().ToString() + ".docx"
            );

            System.IO.File.Copy(templateFilePath, copyFilePath);

            var doc = DocX.Load(copyFilePath);

            doc.ReplaceText(_configuration["PracticeDiary:StudentFCsKeywords"], user.FCs);
            //doc.ReplaceText(_configuration["PracticeDiary:GradeKeywords"], "******"); // user.Grade + " курс", обсудить
            doc.ReplaceText(_configuration["PracticeDiary:CompanyFullNameKeywords"], user.Company.Name);
            //doc.ReplaceText(_configuration["PracticeDiary:OrderKeywords"], "******"); // обсудить
            //doc.ReplaceText(_configuration["PracticeDiary:ManagerFCsKeywords"], "******"); // обсудить
            doc.Save();

            var file = System.IO.File.ReadAllBytes(copyFilePath);
            System.IO.File.Delete(copyFilePath);

            return File(file, 
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
                $"Дневник практики ({user.FCs}, X курс, X семестр).docx"
            );
        }
    }
}