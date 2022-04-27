using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Company;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Dto.Vacancy;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CompanyController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var companyEntity = await _context.Companies.Include(x => x.Vacancies).FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            var vacancyDtos = _mapper.Map<List<VacancyDto>>(companyEntity.Vacancies);

            return Ok(vacancyDtos);
        }


        [HttpGet]
        [Route("{id}/Employees")]
        public async Task<IActionResult> GetEmployees(Guid id)
        {
            var companyEntity = await _context.Companies.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            var fullUserInfoDtos = _mapper.Map<List<FullUserInfoDto>>(companyEntity.Employees);

            return Ok(fullUserInfoDtos);
        }

        [HttpPost]
        [Route("{id}/Employees")]
        public async Task<IActionResult> CreateEmployee(Guid id, [FromBody] EmployeeEditDto employeeEditDto)
        {
            var companyEntity = await _context.Companies.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == employeeEditDto.UserId.ToString());

            if (companyEntity == null || userEntity == null)
                return NotFound();

            if (companyEntity.Employees.Any(x => x.Id == userEntity.Id))
                return Conflict();

            try
            {
                companyEntity.Employees.Add(userEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}/Employees/{userId}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, Guid userId)
        {
            var companyEntity = await _context.Companies.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId.ToString());

            if (companyEntity == null || userEntity == null || !companyEntity.Employees.Any(x => x.Id == userEntity.Id))
                return NotFound();

            try
            {
                companyEntity.Employees.Remove(userEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }
    }
}
