using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class FeedbackContoller : Controller
    {
        private readonly ApplicationDbContext _context;
        public FeedbackContoller(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var feedbackEntities = await _context.Feedbacks.ToListAsync();
            var feedbackDtos = new List<FeedbackDto>();

            foreach (var feedbackEntity in feedbackEntities)
            {
                var feedbackDto = new FeedbackDto()
                {
                    Id = feedbackEntity.Id,
                    CreatedDate = feedbackEntity.CreatedDate,
                };
                feedbackDtos.Add(feedbackDto);
            }

            return Ok(feedbackDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var feedbackEntity = await _context.Feedbacks
                .FirstOrDefaultAsync(x => x.Id == id);

            if (feedbackEntity == null)
                return NotFound();

            var feedbackDto = new FeedbackDto()
            {
                Id = feedbackEntity.Id,
                CreatedDate = feedbackEntity.CreatedDate,
            };

            return Ok(feedbackDto);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] FeedbackCreateEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var feedbackEntity = new Feedback()
                {
                    Text = model.Text,
                    Verdict = model.Verdict,
                    CreatedDate = DateTime.UtcNow,
                };

                _context.Add(feedbackEntity);
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
        public async Task<IActionResult> Put(Guid id, [FromBody] FeedbackCreateEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedbackEntity = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);

            if (feedbackEntity == null)
                return NotFound();

            try
            {
                feedbackEntity.Text = model.Text;
                feedbackEntity.Verdict = model.Verdict;
                feedbackEntity.EditedDate = DateTime.UtcNow;

                _context.Update(feedbackEntity);
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
            var feedbackEntity = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);

            if (feedbackEntity == null)
                return NotFound();

            try
            {
                _context.Remove(feedbackEntity);
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
