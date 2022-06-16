using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Interview;
using TinkoffWatcher_Api.Dto.Vacancy;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class PracticeDiaryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public PracticeDiaryController
        (
            ApplicationDbContext context, 
            IMapper mapper, 
            IConfiguration configuration
        )
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderEntity = _context.Properties.FirstOrDefault(x => x.Name == _configuration["Properties:OrderKeywords"]);
            
            if (orderEntity == null)
                return Ok();
            
            return Ok(orderEntity.Value);
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> Create(string orderNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderEntity = _context.Properties.FirstOrDefault(x => x.Name == _configuration["Properties:OrderKeywords"]);
            if (orderEntity == default)
            {
                orderEntity = new Property()
                {
                    Name = _configuration["Properties:OrderKeywords"],
                };
            }

            orderEntity.Value = orderNumber;

            _context.Properties.Update(orderEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}