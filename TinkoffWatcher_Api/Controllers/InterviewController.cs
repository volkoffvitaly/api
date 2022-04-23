using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Interview;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class InterviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InterviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var interviewEntities = await _context.Interviews
                .Include(_ => _.Student)
                .Include(_ => _.Feedbacks)
                .Include(_ => _.Agents)
                .ToListAsync();
            var interviewDtos = new List<InterviewDto>();

            foreach (var interviewEntity in interviewEntities)
            {
                var interviewDto = new InterviewDto()
                {
                    Id = interviewEntity.Id,
                    CreatedDate = interviewEntity.CreatedDate,
                    AdditionalInfo = interviewEntity.AdditionalInfo,
                    Date = interviewEntity.Date,
                    Student = interviewEntity.Student,
                    Agents = interviewEntity.Agents,
                    Feedbacks = interviewEntity.Feedbacks,
                };
                interviewDtos.Add(interviewDto);
            }

            return Ok(interviewDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var interviewEntity = await _context.Interviews
                .Include(_ => _.Student)
                .Include(_ => _.Feedbacks)
                .Include(_ => _.Agents)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (interviewEntity == null)
                return NotFound();

            var vacancyDto = new InterviewDto()
            {
                Id = interviewEntity.Id,
                CreatedDate = interviewEntity.CreatedDate,
                AdditionalInfo = interviewEntity.AdditionalInfo,
                Date = interviewEntity.Date,
                Student = interviewEntity.Student,
                Agents = interviewEntity.Agents
            };

            return Ok(vacancyDto);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] InterviewCreateEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var interviewEntity = new Interview()
                {
                    Date = model.Date,
                    CreatedDate = DateTime.UtcNow,
                    StudentId = model.StudentId,
                    VacancyId = model.VacancyId
                };

                _context.Add(interviewEntity);
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
        public async Task<IActionResult> Put(Guid id, [FromBody] InterviewCreateEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var interviewEntity = await _context.Interviews.FirstOrDefaultAsync(x => x.Id == id);

            if (interviewEntity == null)
                return NotFound();

            try
            {
                interviewEntity.Date = interviewEntity.Date;
                interviewEntity.AdditionalInfo = interviewEntity.AdditionalInfo;
                interviewEntity.EditedDate = DateTime.UtcNow;

                _context.Update(interviewEntity);
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
            var interviewEntity = await _context.Interviews.FirstOrDefaultAsync(x => x.Id == id);

            if (interviewEntity == null)
                return NotFound();

            try
            {
                _context.Remove(interviewEntity);
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
