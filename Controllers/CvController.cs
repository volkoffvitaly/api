using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Cv;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class CvController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public CvController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var CvEntities = await _context.Cvs.ToListAsync();

            var mapped = new List<CvDto>();
            foreach (var cv in CvEntities)
            {
                mapped.Add(_mapper.Map<CvDto>(cv));
            }

            return Ok(mapped);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCv(Guid id)
        {
            var CvEntity = await _context.Cvs.FirstOrDefaultAsync(cv => cv.Id == id);

            var CvDto = _mapper.Map<CvDto>(CvEntity);

            return Ok(CvDto);
        }

        [HttpGet]
        [Route("CurrentUser")]
        public async Task<IActionResult> GetCv()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return BadRequest("Current user is not detected.");
            }

            var CvEntity = user.Cv;

            if (CvEntity == null)
                return Ok();

            var CvDto = _mapper.Map<CvDto>(CvEntity);

            return Ok(CvDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CvCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cvEntity = _mapper.Map<Cv>(model);

                _context.Update(cvEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpPut]
        [Route("{cvId}")]
        public async Task<IActionResult> Edit(Guid cvId, [FromBody] CvCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cvEntity = _context.Cvs.FirstOrDefault(cv => cv.Id == cvId);

            if (cvEntity == null)
                return NotFound();

            cvEntity = _mapper.Map(model, cvEntity);

            _context.Update(cvEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("{cvId}/WorkExperience/{id}")]
        public async Task<IActionResult> Put(Guid cvId, Guid id, [FromBody] WorkExperienceDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var cvEntity = await _context.Cvs.FirstOrDefaultAsync(x => x.Id == cvId);
            if (cvEntity == null)
                throw new KeyNotFoundException($"Cv with {cvId} not found in DB");

            var workExperienceEntity = await _context.WorkExperiences.FirstOrDefaultAsync(x => x.Id == id);

            if (workExperienceEntity == null)
                return NotFound();

            try
            {
                workExperienceEntity = _mapper.Map(model, workExperienceEntity);

                _context.Update(workExperienceEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpPut]
        [Route("{cvId}/UsefulLink/{id}")]
        public async Task<IActionResult> Put(Guid cvId, Guid id, [FromBody] UsefulLinkDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var cvEntity = await _context.Cvs.FirstOrDefaultAsync(x => x.Id == cvId);
            if (cvEntity == null)
                throw new KeyNotFoundException($"Cv with {cvId} not found in DB");

            var usefulLinkEntity = await _context.UsefulLinks.FirstOrDefaultAsync(x => x.Id == id);

            if (usefulLinkEntity == null)
                return NotFound();

            try
            {
                usefulLinkEntity = _mapper.Map(model, usefulLinkEntity);

                _context.Update(usefulLinkEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpPut]
        [Route("{cvId}/LanguageProficiency/{id}")]
        public async Task<IActionResult> Put(Guid cvId, Guid id, [FromBody] LanguageProficiencyDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var cvEntity = await _context.Cvs.FirstOrDefaultAsync(x => x.Id == cvId);
            if (cvEntity == null)
                throw new KeyNotFoundException($"Cv with {cvId} not found in DB");

            var languageProficiencyEntity = await _context.LanguageProficiencies.FirstOrDefaultAsync(x => x.Id == id);

            if (languageProficiencyEntity == null)
                return NotFound();

            try
            {
                languageProficiencyEntity = _mapper.Map(model, languageProficiencyEntity);

                _context.Update(languageProficiencyEntity);
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
            var cvEntity = await _context.Cvs.FirstOrDefaultAsync(x => x.Id == id);

            if (cvEntity == null)
                return NotFound();

            try
            {
                _context.LanguageProficiencies.RemoveRange(cvEntity.LanguageProficiencies);
                _context.WorkExperiences.RemoveRange(cvEntity.WorkExperiences);
                _context.UsefulLinks.RemoveRange(cvEntity.UsefulLinks);
                _context.Cvs.Remove(cvEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{cvId}/WorkExperience/{id}")]
        public async Task<IActionResult> RemoveWorkExperience(Guid cvId, Guid id)
        {
            var cvEntity = await _context.Cvs.FirstOrDefaultAsync(x => x.Id == cvId);

            if (cvEntity == null)
                return NotFound();

            var workExpEntity = cvEntity.WorkExperiences.FirstOrDefault(we => we.Id == id);

            if (workExpEntity == null)
                return NotFound();

            try
            {
                _context.Remove(workExpEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{cvId}/UsefulLink/{id}")]
        public async Task<IActionResult> RemoveUsefulLink(Guid cvId, Guid id)
        {
            var cvEntity = await _context.Cvs.FirstOrDefaultAsync(x => x.Id == cvId);

            if (cvEntity == null)
                return NotFound();

            var usefulLinkEntity = cvEntity.UsefulLinks.FirstOrDefault(ul => ul.Id == id);

            if (usefulLinkEntity == null)
                return NotFound();

            try
            {
                _context.Remove(usefulLinkEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{cvId}/LanguageProficiency/{id}")]
        public async Task<IActionResult> RemoveLanguageProficiency(Guid cvId, Guid id)
        {
            var cvEntity = await _context.Cvs.FirstOrDefaultAsync(x => x.Id == cvId);

            if (cvEntity == null)
                return NotFound();

            var languageProfEntity = cvEntity.LanguageProficiencies.FirstOrDefault(lp => lp.Id == id);

            if (languageProfEntity == null)
                return NotFound();

            try
            {
                _context.Remove(languageProfEntity);
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
