using AutoMapper;
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
        private readonly IMapper _mapper;
        public VacancyController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vacancyEntity = await _context.Vacancies.FirstOrDefaultAsync(x => x.Id == id);

            if (vacancyEntity == null)
                return NotFound();

            var vacancyDto = _mapper.Map<VacancyDto>(vacancyEntity);

            return Ok(vacancyDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VacancyEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vacancyEntity = _mapper.Map<Vacancy>(model);
                vacancyEntity.CreatedDate = DateTime.UtcNow;

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
        public async Task<IActionResult> Put(Guid id, [FromBody] VacancyEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vacancyEntity = await _context.Vacancies.FirstOrDefaultAsync(x => x.Id == id);

            if (vacancyEntity == null)
                return NotFound();

            try
            {
                vacancyEntity = _mapper.Map(model, vacancyEntity);
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
