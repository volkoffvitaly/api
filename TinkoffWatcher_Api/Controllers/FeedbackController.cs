using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FeedbackController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var feedbackEntities = _context.Feedbacks;
            var feedbackDtos = _mapper.ProjectTo<FeedbackDto>(feedbackEntities);

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

            var feedbackDto = _mapper.Map<FeedbackDto>(feedbackEntity);

            return Ok(feedbackDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeedbackCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var feedbackEntity = _mapper.Map<Feedback>(model);
                feedbackEntity.CreatedDate = DateTime.UtcNow;

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
        public async Task<IActionResult> Put(Guid id, [FromBody] FeedbackEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedbackEntity = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);

            if (feedbackEntity == null)
                return NotFound();

            try
            {
                feedbackEntity = _mapper.Map(model, feedbackEntity);
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