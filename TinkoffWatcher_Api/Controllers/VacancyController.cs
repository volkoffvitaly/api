using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Vacancy;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class VacancyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VacancyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vacancyEntity = await _context.Vacancies.FirstOrDefaultAsync(x => x.Id == id);

            if (vacancyEntity == null)
                return NotFound();

            var vacancyDto = new VacancyDto()
            {
                Id = vacancyEntity.Id,
                Name = vacancyEntity.Name,
                Description = vacancyEntity.Description,
                CreatedDate = vacancyEntity.CreatedDate
            };

            return Ok(vacancyDto);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] VacancyCreateEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vacancyEntity = new Vacancy()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedDate = DateTime.UtcNow,
                    PositionAmount = model.PositionAmount,
                    CompanyId = model.CompanyId
                };

                _context.Add(vacancyEntity);
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
        public async Task<IActionResult> Put(Guid id, [FromBody] VacancyCreateEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vacancyEntity = await _context.Vacancies.FirstOrDefaultAsync(x => x.Id == id);

            if (vacancyEntity == null)
                return NotFound();

            try
            {
                vacancyEntity.Name = model.Name;
                vacancyEntity.Description = model.Description;
                vacancyEntity.EditedDate = DateTime.UtcNow;

                _context.Update(vacancyEntity);
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
            var vacancyEntity = await _context.Vacancies.FirstOrDefaultAsync(x => x.Id == id);

            if (vacancyEntity == null)
                return NotFound();

            try
            {
                _context.Remove(vacancyEntity);
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
