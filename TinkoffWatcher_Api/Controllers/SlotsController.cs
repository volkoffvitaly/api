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
using TinkoffWatcher_Api.Dto.Slot;
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
    public class SlotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public SlotsController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var slotEntity = await _context.Slots.FirstOrDefaultAsync(x => x.Id == id);

            if (slotEntity == null)
                return NotFound();

            var slotDto = _mapper.Map<SlotDto>(slotEntity);

            return Ok(slotDto);
        }

        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] SlotsFilter filter)
        {
            var filterPredicate = GenerateFilterPredicate(filter);
            var slotEntities = _context.Slots.Where(filterPredicate).OrderBy(x => x.DateTime);
            var slotsDtos = _mapper.ProjectTo<SlotDto>(slotEntities);

            return Ok(slotsDtos);
        }

        private static Expression<Func<Slot, bool>> GenerateFilterPredicate(SlotsFilter filter)
        {
            Expression<Func<Slot, bool>> expr = request => true;

            if (!filter.StartDate.HasValue)
            {
                var filterDate = DateTime.UtcNow.Date;
                expr = expr.AndAlso(slot => slot.DateTime >= filterDate);
            }
            else
                expr = expr.AndAlso(slot => slot.DateTime >= ((DateTime)filter.StartDate).Date);

            if (filter.VacancyId.HasValue)
                expr = expr.AndAlso(slot => slot.VacancyId == filter.VacancyId);

            return expr;
        }
        
        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Create([FromBody] SlotEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var slotEntity = _mapper.Map<Slot>(model);
            slotEntity.DateTime = model.DateTime;

            _context.Add(slotEntity);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<SlotDto>(slotEntity));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Put(Guid id, [FromBody] SlotEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var slotEntity = await _context.Slots.FirstOrDefaultAsync(x => x.Id == id);

            if (slotEntity == null)
                return NotFound();

            slotEntity = _mapper.Map(model, slotEntity);
            slotEntity.DateTime = model.DateTime;

            _context.Update(slotEntity);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<SlotDto>(slotEntity));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.CompanyAgent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var slotEntity = await _context.Slots.FirstOrDefaultAsync(x => x.Id == id);

            if (slotEntity == null)
                return NotFound();

            _context.Remove(slotEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}