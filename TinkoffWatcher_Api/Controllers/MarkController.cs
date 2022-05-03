using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
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
            var markEntity = await _context.Marks.FirstOrDefaultAsync(x => x.Id == id);

            if (markEntity == null)
                return NotFound();

            var markDto = _mapper.Map<MarkDto>(markEntity);

            return Ok(markDto);
        }

        [HttpPost]
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