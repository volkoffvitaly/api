using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Company;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyEntity == null)
                return NotFound();

            var companyDto = new CompanyDto()
            {
                Id = companyEntity.Id,
                Name = companyEntity.Name,
                Description = companyEntity.Description,
                CreatedDate = companyEntity.CreatedDate,
                EditedDate = companyEntity.EditedDate,
            };

            return Ok(companyDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyEditDto companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var companyEntity = new Company()
                {
                    Name = companyDto.Name,
                    Description = companyDto.Description,
                    CreatedDate = DateTime.UtcNow,
                };

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
                companyEntity.Name = companyDto.Name;
                companyEntity.Description = companyDto.Description;
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
    }
}
