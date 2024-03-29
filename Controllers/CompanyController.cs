﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Company;
using TinkoffWatcher_Api.Dto.Interview;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Dto.Vacancy;
using TinkoffWatcher_Api.Filters;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CompanyController(
            ApplicationDbContext context, 
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companyEntities = _context.Companies;
            var companyDtos = _mapper.ProjectTo<CompanyDto>(companyEntities);

            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            var companyDto = _mapper.Map<CompanyDto>(companyEntity);

            return Ok(companyDto);
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> Create([FromBody] CompanyEditDto companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var companyEntity = _mapper.Map<Company>(companyDto);
                companyEntity.CreatedDate = DateTime.UtcNow;

                _context.Add(companyEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }
                

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> Put(Guid id, [FromBody] CompanyEditDto companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            try
            {
                companyEntity = _mapper.Map(companyDto, companyEntity);
                companyEntity.EditedDate = DateTime.UtcNow;

                _context.Update(companyEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            try
            {
                _context.Remove(companyEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpGet]
        [Route("{id}/Vacancies")]
        public async Task<IActionResult> GetVacancies(Guid id)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            var vacancyDtos = _mapper.Map<List<VacancyDto>>(companyEntity.Vacancies);

            return Ok(vacancyDtos);
        }


        [HttpGet]
        [Route("{id}/Employees")]
        public async Task<IActionResult> GetEmployees(Guid id, [FromQuery] UserFilter userFilter)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            var users = string.IsNullOrWhiteSpace(userFilter.Role) ?
                companyEntity.Employees :
                companyEntity.Employees.Where(x => _userManager.IsInRoleAsync(x, userFilter.Role).GetAwaiter().GetResult());

            var fullUserInfoDtos = _mapper.Map<List<FullUserInfoDto>>(users);

            return Ok(fullUserInfoDtos);
        }
                
        [HttpPost]
        [Route("{id}/Employees")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> AddEmployee(Guid id, [FromBody] EmployeeEditDto employeeEditDto)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound("Company wasn't found. Id: " + id);

            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == employeeEditDto.UserId);

            if (userEntity == null)
                return NotFound("User wasn't found. Id: " + employeeEditDto.UserId);

            if (companyEntity.Employees.Select(x => x.Id).Contains(employeeEditDto.UserId))
                return Conflict("User with such Id attached to this Company already. Id: " + employeeEditDto.UserId);

            userEntity.Company = companyEntity;
            userEntity.Post = employeeEditDto.Post;

            _context.Update(userEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}/Employees/{userId}")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> RemoveEmployee(Guid id, Guid userId)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound("Company wasn't found. Id: " + id);

            var userEntity = companyEntity.Employees.FirstOrDefault(x => x.Id == userId);

            if (userEntity == null)
                return NotFound("User wasn't found. Id: " + userId);

            companyEntity.Employees.Remove(userEntity);
            _context.Update(companyEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("{id}/Subscribe")]
        public async Task<IActionResult> SubscribeToCompany (Guid id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                //НУ тут надо приделать нормальное сообщение о том, что юзер не ровный поцик
                throw new Exception("Strange user");
            }

            try
            {
                var subscription = new SubscriberToCompany();
                var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

                if(user.Subscriptions == null)
                {
                    user.Subscriptions = new List<SubscriberToCompany>();
                }
                if(user.Subscriptions.FirstOrDefault(_ => _.CompanyId == id) != null)
                {
                    return BadRequest("You already subscribed on this company");
                }
                subscription.Company = companyEntity;
                user.Subscriptions.Add(subscription);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                //Надо будет других катчей накинуть
                throw new Exception();
            }
        }


        [HttpPost]
        [Route("{id}/Unsubscribe")]
        public async Task<IActionResult> UnsubscribeToCompany(Guid id)

        {
            var userName = User.Identity.Name;
            var aspNetUser = await _userManager.FindByNameAsync(userName);
            if (aspNetUser == null)
            {
                //НУ тут надо приделать нормальное сообщение о том, что юзер не ровный поцик
                throw new Exception("Strange user");
            }

            try
            {
                var subscription = await _context.SubscriberToCompanies
                    .FirstOrDefaultAsync(x => x.SubscriberId == aspNetUser.Id && x.CompanyId == id);
                
                if(subscription == null)
                {
                    return NotFound($"Company with id={id} not found");
                }

                _context.Remove(subscription);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                //Надо будет других катчей накинуть
                throw new Exception();
            }
        }
    }
}
