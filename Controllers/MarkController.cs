using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.Mark;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Enums;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MarkController(ApplicationDbContext context, 
            IMapper mapper, 
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
            var markEntities = await _context.Marks.Where(GenerateFilterPredicate(filter)).ToListAsync();
            var marksDtos = markEntities.Select(_mapper.Map<MarkDto>).ToList();

            return Ok(marksDtos);
        }

        private static Expression<Func<Mark, bool>> GenerateFilterPredicate(MarksFilter filter)
        {
            Expression<Func<Mark, bool>> expr = request => true;

            if (!string.IsNullOrEmpty(filter.Value))
                expr = expr.AndAlso(mark => mark.OverallMark == filter.Value);

            if (filter.Semester.HasValue)
                expr = expr.AndAlso(mark => mark.Semester == filter.Semester);

            if (filter.StartYear.HasValue && filter.EndYear.HasValue)
                expr = expr.AndAlso(mark => filter.StartYear <= mark.Year && mark.Year <= filter.EndYear);

            return expr;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] MarkEditDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var markEntity = new Mark() {
                OverallMark = model.OverallMark,
                Semester = model.Semester,
                AdditionalComment = model.AdditionalComment,
                Year = model.Year,
                StudentId = model.StudentId,
                AgentId = model.AgentId,
                Characteristics = new List<Characteristic>()
            };

            var ansersEntities = await _context.CharacteristicAnswers
                .Where(_ => _.IsCurrent && !_.IsDeleted).ToListAsync();

            foreach (var characteristic in model.Characteristics)
            {
                var answerEntities = new List<CharacteristicAnswer>();

                foreach(var answer in characteristic.CharacteristicAnswerIds)
                {
                    answerEntities.Add(ansersEntities.FirstOrDefault(_ => _.Id == answer));
                }

                markEntity.Characteristics.Add(new Characteristic()
                {
                    Other = characteristic.Other,
                    CharacteristicQuestionId = characteristic.CharacteristicQuestionId,
                    CharacteristicAnswers = answerEntities
                });
            }

            _context.Add(markEntity);
            await _context.SaveChangesAsync();

            var markDto = _mapper.Map<MarkDto>(markEntity);

            return Ok(markDto);
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

            var ansersEntities = await _context.CharacteristicAnswers.ToListAsync();

            foreach (var characteristic in model.Characteristics)
            {
                var answerEntities = new List<CharacteristicAnswer>();

                foreach (var answer in characteristic.CharacteristicAnswerIds)
                {
                    answerEntities.Add(ansersEntities.FirstOrDefault(_ => _.Id == answer));
                }

                markEntity.Characteristics.Add(new Characteristic()
                {
                    Other = characteristic.Other,
                    CharacteristicQuestionId = characteristic.CharacteristicQuestionId,
                    CharacteristicAnswers = answerEntities
                });
            }

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
        [Route("CharacteristicQuestion")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCharacteristicTypes()
        {
            try
            {
                var characteristicTypes = await _context.CharacteristicQuestions
                    .Include(_ => _.CharacteristicAnswers)
                    .Where(_ => _.IsCurrent && !_.IsDeleted)
                    .ToListAsync();

                var result = new List<CharacteristicQuestionDto>();

                foreach (var type in characteristicTypes)
                {
                    type.CharacteristicAnswers = type.CharacteristicAnswers
                        .Where(_ => _.IsCurrent && !_.IsDeleted).ToList();
                    var mapped = _mapper.Map<CharacteristicQuestionDto>(type);
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
        [Route("CharacteristicQuestion")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCharacteristicType(CharacteristicQuestionCreateDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var characteristicAnswers = new List<CharacteristicAnswer>();
                foreach(var answer in model.CharacteristicAnswers)
                {
                    characteristicAnswers.Add(new CharacteristicAnswer() 
                    { Description = answer.Description });
                }

                var characteristicType = new CharacteristicQuestion()
                {
                    Name = model.Name,
                    IsMultipleValues = model.IsMultipleValues,
                    CharacteristicAnswers = characteristicAnswers
                };

                _context.CharacteristicQuestions.Add(characteristicType);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Something went wrong. May be try later");
            }

            return Ok();
        }

        [HttpPut]
        [Route("CharacteristicQuestion/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> EditCharacteristicType(Guid id, CharacteristicQuestionEditDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var characteristicTypeEntity = await _context.CharacteristicQuestions
                    .Include(_ => _.CharacteristicAnswers)
                    .FirstOrDefaultAsync(_ => _.Id == id);

                if(characteristicTypeEntity == null)
                {
                    return BadRequest($"CharacteristicType with id={id} not found in DB");
                }

                var answerEntities = characteristicTypeEntity.CharacteristicAnswers;
                foreach (var answer in model.CharacteristicAnswers)
                {
                    var answerEntity = characteristicTypeEntity.CharacteristicAnswers.FirstOrDefault(_ => _.Id == answer.Id);
                    if(answerEntity == null)
                    {
                        return BadRequest($"Characteristic anwswer with id={answer.Id} not found in DB");
                    }
                    answerEntity.Description = answer.Description;
                }

                characteristicTypeEntity.Name = model.Name;
                characteristicTypeEntity.CharacteristicAnswers = answerEntities;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Something went wrong. May be try later");
            }

            return Ok();
        }

        [HttpDelete]
        [Route("CharacteristicQuestion/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveCharacteristicType(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var characteristicTypeEntity = await _context.CharacteristicQuestions
                    .Include(_ => _.CharacteristicAnswers)
                    .FirstOrDefaultAsync(_ => _.Id == id);

                if (characteristicTypeEntity == null)
                {
                    return BadRequest($"CharacteristicType with id={id} not found in DB");
                }

                _context.CharacteristicQuestions.Remove(characteristicTypeEntity);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Something went wrong. May be try later");
            }

            return Ok();
        }

        [HttpDelete]
        [Route("CharacteristicQuestion/{id}/{valueId}")]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveCharacteristicTypeValue(Guid id, Guid valueId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var characteristicValueEntity = await _context.CharacteristicAnswers
                    .FirstOrDefaultAsync(_ => _.Id == valueId && _.CharacteristicQuestionId == id);

                if (characteristicValueEntity == null)
                {
                    return BadRequest($"Characteristic value with id={valueId} not found in DB");
                }

                _context.CharacteristicAnswers.Remove(characteristicValueEntity);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Something went wrong. May be try later");
            }

            return Ok();
        }

        [HttpGet]
        [Route("MarksReport")]
        [Authorize(Roles = ApplicationRoles.Administrators + "," + ApplicationRoles.SchoolAgent)]
        public async Task<IActionResult> GetMarksReport()
        {
            var templateFilePath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                _configuration["WwwrootPathKeys:Files"],
                _configuration["WwwrootPathKeys:MarksReportTemplate"]
            );

            var copyFilePath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                _configuration["WwwrootPathKeys:Files"],
                Guid.NewGuid().ToString() + ".xlsx"
            );

            System.IO.File.Copy(templateFilePath, copyFilePath);

            var workbook = new XLWorkbook(copyFilePath);
            var worksheets = workbook.Worksheets.OrderBy(x => x.Name);

            foreach (var worksheet in worksheets)
            {
                var grade = worksheet.Name[0];
                var students = _context.Users
                    .Where(x => x.Grade == (Grade)grade)
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ThenBy(x => x.MiddleName);

                var rowPointer = worksheet.FirstRow();

                foreach (var student in students)
                {
                    worksheet.Range(worksheet.Cell(rowPointer.RowNumber(), 1), worksheet.Cell(rowPointer.RowNumber(), 50)).Merge();
                    rowPointer.Cell(1).SetValue($"{student.FCs} ({student.MarksAsStudent.Count} оценок)").Style.Font.SetBold();
                    rowPointer = rowPointer.RowBelow();

                    foreach (var mark in student.MarksAsStudent)
                    {
                        rowPointer.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        rowPointer.RowBelow().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        var semesterName = mark.Semester == SemesterEnum.Spring ? "Весенний" : "Осенний";

                        rowPointer.Cell(1).SetValue("Год / Семестр");
                        rowPointer.RowBelow().Cell(1).SetValue($"{mark.Year} / {semesterName}");
                        rowPointer.Cell(2).SetValue("Оценка");
                        rowPointer.RowBelow().Cell(2).SetValue(mark.OverallMark);
                        rowPointer.Cell(3).SetValue("Компания");
                        rowPointer.RowBelow().Cell(3).SetValue(student.Company.Name);
                        rowPointer.Cell(4).SetValue("Выставил");
                        rowPointer.RowBelow().Cell(4).SetValue(mark.Agent.FCs);
                        rowPointer.Cell(5).SetValue("Кем приходится студенту");
                        rowPointer.RowBelow().Cell(5).SetValue(mark.Agent.Post);
                        rowPointer.Cell(6).SetValue("Комментарий");
                        rowPointer.RowBelow().Cell(6).SetValue(mark.AdditionalComment);

                        var cellPointer = 7;

                        foreach (var characteristic in mark.Characteristics)
                        {
                            var offset = 0;

                            //if (characteristic.CharacteristicValues is CharacteristicBoolValue boolValue)
                            //{
                            //    rowPointer.RowBelow().Cell(cellPointer).SetValue(boolValue.BoolValue);

                            //    offset = 1;
                            //}

                            //if (characteristic.CharacteristicValues.FirstOrDefault() is CharacteristicIntValue intValue)
                            //{
                            //    worksheet.Range(worksheet.Cell(rowPointer.RowNumber(), cellPointer), worksheet.Cell(rowPointer.RowNumber(), cellPointer + 1)).Merge();

                            //    rowPointer.RowBelow().Cell(cellPointer).SetValue(intValue.IntValue);
                            //    rowPointer.RowBelow().Cell(cellPointer + 1).SetValue(intValue.Description);

                            //    if (!string.IsNullOrWhiteSpace(characteristic.Other))
                            //    {
                            //        rowPointer.RowBelow().Cell(cellPointer).SetValue("[Другое]");
                            //        rowPointer.RowBelow().Cell(cellPointer + 1).SetValue(characteristic.Other);
                            //    }

                            //    offset = 2;
                            //}

                            rowPointer.Cell(cellPointer).SetValue(characteristic.CharacteristicQuestion.Name);

                            cellPointer += offset;
                        }

                        rowPointer = rowPointer.RowBelow(3);
                    }

                    if (student.MarksAsStudent == default || !student.MarksAsStudent.Any())
                        rowPointer = rowPointer.RowBelow(1);
                }
            }

            workbook.Save();

            var file = System.IO.File.ReadAllBytes(copyFilePath);
            System.IO.File.Delete(copyFilePath);

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Оценки за практику ({DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}).xlsx"
            );
        }
    }
}