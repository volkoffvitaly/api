using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Extensions;
using TinkoffWatcher_Api.Filters;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarkController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public MarkController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var markEntity = await _context.Marks
                .Include(x => x.Agent)
                .Include(x => x.Student)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (markEntity == null)
                return NotFound();

            var markDto = _mapper.Map<MarkDto>(markEntity);

            return Ok(markDto);
        }

        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] MarksFilter filter)
        {
            var markEntities = _context.Marks
                .Include(x => x.Agent)
                .Include(x => x.Student)
                .Where(GenerateFilterPredicate(filter));
            var marksDtos = _mapper.ProjectTo<MarkDto>(markEntities);

            return Ok(marksDtos);
        }

        private static Expression<Func<Mark, bool>> GenerateFilterPredicate(MarksFilter filter)
        {
            Expression<Func<Mark, bool>> expr = request => true;

            if (filter.Value.HasValue)
                expr = expr.AndAlso(mark => mark.Value == filter.Value);

            if (filter.Semester.HasValue)
                expr = expr.AndAlso(mark => mark.Semester == filter.Semester);

            if (filter.StartYear.HasValue && filter.EndYear.HasValue)
                expr = expr.AndAlso(mark => filter.StartYear <= mark.Year && mark.Year <= filter.EndYear);

            return expr;
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Create([FromBody] MarkEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                        
            var markEntity = _mapper.Map<Mark>(model);

            _context.Add(markEntity);
            await _context.SaveChangesAsync();

            return Ok(markEntity);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Put(Guid id, [FromBody] MarkEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var markEntity = await _context.Marks.FirstOrDefaultAsync(x => x.Id == id);

            if (markEntity == null)
                return NotFound();

            markEntity = _mapper.Map(model, markEntity);

            _context.Update(markEntity);
            await _context.SaveChangesAsync();

            return Ok(markEntity);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var markEntity = await _context.Marks.FirstOrDefaultAsync(x => x.Id == id);

            if (markEntity == null)
                return NotFound();
                        
            _context.Remove(markEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}