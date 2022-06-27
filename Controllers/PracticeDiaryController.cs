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
using TinkoffWatcher_Api.Dto.Slot;
using TinkoffWatcher_Api.Dto.Vacancy;
using TinkoffWatcher_Api.Enums;
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
        public async Task<IActionResult> Get(Grade? grade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diarySettngsEntities = _context.DiarySettings.ToList();

            if (diarySettngsEntities == null || !diarySettngsEntities.Any())
                return Ok();

            var diarySettngsDtos = _mapper.Map<List<DiarySettingsDto>>(diarySettngsEntities);

            if (grade != null)
                return Ok(diarySettngsDtos.FirstOrDefault(x => x.Grade == grade));

            return Ok(diarySettngsDtos);
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> Create(DiarySettingsEditDto diarySettingsEditDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diarySettngsEntity = _context.DiarySettings.FirstOrDefault(x => x.Grade == diarySettingsEditDto.Grade);

            if (diarySettngsEntity == null)
            {
                diarySettngsEntity = _mapper.Map<DiarySettings>(diarySettingsEditDto);
            }
            else
            {
                diarySettngsEntity = _mapper.Map(diarySettingsEditDto, diarySettngsEntity);
            }
            _context.DiarySettings.Update(diarySettngsEntity);
            await _context.SaveChangesAsync();

            diarySettngsEntity = _context.DiarySettings.FirstOrDefault(x => x.Grade == diarySettingsEditDto.Grade);

            var diarySettngsDto = _mapper.Map<DiarySettingsDto>(diarySettngsEntity);

            return Ok(diarySettngsDto);
        }
    }
}