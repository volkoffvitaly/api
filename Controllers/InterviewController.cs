﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.Interview;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class InterviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public InterviewController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var interviewEntities = _context.Interviews.Include(_ => _.Student);
            var interviewDtos = await _mapper.ProjectTo<InterviewDto>(interviewEntities).ToListAsync();

            return Ok(interviewDtos);
        }

        [HttpGet]
        [Route("{id}/Feedbacks")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> GetFeedbacks(Guid id)
        {
            var interviewEntity = await _context.Interviews.FirstOrDefaultAsync(x => x.Id == id);
            var feedbacksDtos = _mapper.Map<List<FeedbackDto>>(interviewEntity.Feedbacks);

            return Ok(feedbacksDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var interviewEntity = await _context.Interviews.FirstOrDefaultAsync(x => x.Id == id);

            if (interviewEntity == null)
                return NotFound();

            var interviewDto = _mapper.Map<InterviewDto>(interviewEntity);

            return Ok(interviewDto);
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Create([FromBody] InterviewCreateOrEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var interviewEntity = _mapper.Map<Interview>(model);

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
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent + "," + ApplicationRoles.Student)]
        public async Task<IActionResult> Put(Guid id, [FromBody] InterviewCreateOrEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var interviewEntity = await _context.Interviews.FirstOrDefaultAsync(x => x.Id == id);

            if (interviewEntity == null)
                return NotFound();

            try
            {
                interviewEntity = _mapper.Map(model, interviewEntity);

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
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var interviewEntity = await _context.Interviews.FirstOrDefaultAsync(x => x.Id == id);

            if (interviewEntity == null)
                return NotFound();

            try
            {
                _context.Feedbacks.RemoveRange(interviewEntity.Feedbacks);
                _context.Interviews.Remove(interviewEntity);
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