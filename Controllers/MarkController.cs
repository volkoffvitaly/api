using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
using TinkoffWatcher_Api.Dto.Mark;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public MarkController(ApplicationDbContext context, 
            IMapper mapper, 
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] MarksFilter filter)
        {
            var markEntities = _context.Marks.Where(GenerateFilterPredicate(filter));
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

            var characteristicTypes = await _context.CharacteristicTypes
                .AsNoTracking()
                .Include(_ => _.CharacteristicValues)
                .Where(_ => _.IsCurrent)
                .ToListAsync();

            var markEntity = new Mark() {
                OverallMark = model.OverallMark,
                Semester = model.Semester,
                AdditionalComment = model.AdditionalComment,
                Year = model.Year,
                StudentId = model.StudentId,
                AgentId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id
            };

            foreach (var characteristic in model.Characteristics)
            {
                var characteristicType = characteristicTypes.FirstOrDefault(_ => _.Id == characteristic.CharacteristicType.Id);

                if (characteristicType == null)
                {
                    return BadRequest($"CharacteristicType with id={characteristic.CharacteristicType.Id} not found in DB");
                }

                var characteristicValue = characteristicType.CharacteristicValues.FirstOrDefault(_ => _.Id == characteristic.CharacteristicValue.Id);
                
                if (characteristicValue == null)
                {
                    return BadRequest($"CharacteristicValue with id={characteristic.CharacteristicValue.Id} not found in DB");
                }

                var characteristickEntity = new Characteristic()
                {
                    CharacteristicType = characteristicType
                };

                if (characteristic.CharacteristicValue.BoolValue != null)
                {
                    characteristickEntity.CharacteristicValue = 
                        _mapper.Map<CharacteristicBoolValue>(characteristic.CharacteristicValue);
                }
                else
                {
                    characteristickEntity.CharacteristicValue =
                        _mapper.Map<CharacteristicIntValue>(characteristic.CharacteristicValue);
                }

                markEntity.Characteristics.Add(characteristickEntity);

            }

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

        [HttpGet]
        [Route("CharacteristicType")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCharacteristicTypes()
        {
            try
            {
                var characteristicTypes = await _context.CharacteristicTypes
                    .Include(_ => _.CharacteristicValues)
                    .Where(_ => _.IsCurrent)
                    .ToListAsync();

                var result = new List<CharacteristicTypeDto>();

                foreach (var type in characteristicTypes)
                {
                    var mapped = _mapper.Map<CharacteristicTypeDto>(type);
                    result.Add(mapped);    
                }

                return Ok(result);
            }
            catch
            {
                throw new Exception("Something went wrong. May be try later");
            }
        }

        [HttpPost]
        [Route("CharacteristicType")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCharacteristicType(CharacteristicTypeDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var charasteristicValues = new List<CharacteristicValue>();
                foreach(var value in model.CharacteristicValues)
                {
                    if (value.BoolValue != null)
                    {
                        charasteristicValues.Add(new CharacteristicBoolValue()
                        {
                            BoolValue = value.BoolValue,
                            Description = value.Description
                        });
                    }
                    else
                    {
                        charasteristicValues.Add(new CharacteristicIntValue()
                        {
                            IntValue = value.IntValue,
                            Description = value.Description
                        });
                    }
                }

                var characteristicType = new CharacteristicType()
                {
                    Name = model.Name,
                    CharacteristicValues = charasteristicValues
                };

                _context.CharacteristicTypes.Add(characteristicType);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Something went wrong. May be try later");
            }

            return Ok();
        }

        [HttpPost]
        [Route("CharacteristicType/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> EditCharacteristicType(Guid id,CharacteristicTypeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var characteristicTypeEntity = await _context.CharacteristicTypes
                    .Include(_ => _.CharacteristicValues)
                    .FirstOrDefaultAsync(_ => _.Id == id);

                if(characteristicTypeEntity == null)
                {
                    return BadRequest($"CharacteristicType with id={id} not found in DB");
                }

                var charasteristicValues = new List<CharacteristicValue>();
                foreach (var value in model.CharacteristicValues)
                {
                    if (value.BoolValue != null)
                    {
                        charasteristicValues.Add(new CharacteristicBoolValue()
                        {
                            BoolValue = value.BoolValue,
                            Description = value.Description
                        });
                    }
                    else
                    {
                        charasteristicValues.Add(new CharacteristicIntValue()
                        {
                            IntValue = value.IntValue,
                            Description = value.Description
                        });
                    }
                }

                characteristicTypeEntity.Name = model.Name;
                characteristicTypeEntity.CharacteristicValues = charasteristicValues;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Something went wrong. May be try later");
            }

            return Ok();
        }
    }
}