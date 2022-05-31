using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinkoffWatcher_Api.Dto.Company;
using TinkoffWatcher_Api.Dto.Cv;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.Interview;
using TinkoffWatcher_Api.Dto.Mark;
using TinkoffWatcher_Api.Dto.Slot;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Dto.Vacancy;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile(UserManager<ApplicationUser> _userManager)
        {
            CreateMap<FullUserInfoEditDto, ApplicationUser>();
            CreateMap<ApplicationUser, FullUserInfoDto>()
                .ForMember(x => x.MarksAsStudent, opt => opt.MapFrom(x => x.MarksAsStudent))
                .ForMember(x => x.MarksAsAgent, opt => opt.MapFrom(x => x.MarksAsAgent))
                .ForMember(x => x.Company, opt => opt.MapFrom(x => x.Company))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => _userManager.GetRolesAsync(x).GetAwaiter().GetResult()));

            CreateMap<CompanyEditDto, Company>();
            CreateMap<Company, CompanyDto>();

            CreateMap<FeedbackEditDto, Feedback>();
            CreateMap<FeedbackCreateDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>();

            CreateMap<InterviewEditDto, Interview>();
            CreateMap<Interview, InterviewDto>();

            CreateMap<VacancyEditDto, Vacancy>();
            CreateMap<Vacancy, VacancyDto>();

            CreateMap<MarkEditDto, Mark>();
            CreateMap<Mark, MarkDto>();

            CreateMap<SlotEditDto, Slot>();
            CreateMap<Slot, SlotDto>();

            CreateMap<WorkExperience, WorkExperienceDto>();
            CreateMap<WorkExperienceDto, WorkExperience>();
            CreateMap<WorkExperienceCreateDto, WorkExperience>();


            CreateMap<LanguageProficiency, LanguageProficiencyDto>()
                .ForMember(lp => lp.Language, opt => opt.MapFrom(x => x.Language));
            CreateMap<LanguageProficiencyDto, LanguageProficiency>()
                .ForMember(lp => lp.Language, opt => opt.MapFrom(x => x.Language));
            CreateMap<LanguageProficiencyCreateDto, LanguageProficiency>()
                .ForMember(lp => lp.Language, opt => opt.MapFrom(x => x.Language));

            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();

            CreateMap<UsefulLink, UsefulLinkDto>();
            CreateMap<UsefulLinkDto, UsefulLink>();
            CreateMap<UsefulLinkCreateDto, UsefulLink>();

            CreateMap<Cv, CvDto>()
                .ForMember(cv => cv.WorkExperiences, opt => opt.MapFrom(x => x.WorkExperiences))
                .ForMember(cv => cv.UsefulLinks, opt => opt.MapFrom(x => x.UsefulLinks))
                .ForMember(cv => cv.LanguageProficiencies, opt => opt.MapFrom(x => x.LanguageProficiencies));
            CreateMap<CvDto, Cv>()
                .ForMember(cv => cv.WorkExperiences, opt => opt.MapFrom(x => x.WorkExperiences))
                .ForMember(cv => cv.UsefulLinks, opt => opt.MapFrom(x => x.UsefulLinks))
                .ForMember(cv => cv.LanguageProficiencies, opt => opt.MapFrom(x => x.LanguageProficiencies));
            CreateMap<CvCreateDto, Cv>()
                .ForMember(cv => cv.WorkExperiences, opt => opt.MapFrom(x => x.WorkExperiences))
                .ForMember(cv => cv.UsefulLinks, opt => opt.MapFrom(x => x.UsefulLinks))
                .ForMember(cv => cv.LanguageProficiencies, opt => opt.MapFrom(x => x.LanguageProficiencies));

            CreateMap<CharacteristicValue, CharacteristicValueDto>();

            CreateMap<CharacteristicBoolValue, CharacteristicValueDto>()
                .IncludeBase< CharacteristicValue, CharacteristicValueDto>();
            CreateMap<CharacteristicIntValue, CharacteristicValueDto>()
                .IncludeBase<CharacteristicValue, CharacteristicValueDto>();


            CreateMap<CharacteristicType, CharacteristicTypeDto>()
                .ForMember(_ => _.CharacteristicValues, opt => opt.MapFrom(x => x.CharacteristicValues));

            CreateMap<Characteristic, CharacteristicDto>()
                .ForMember(_ => _.CharacteristicType, opt => opt.MapFrom(x => x.CharacteristicType));

            CreateMap<CharacteristicValueDto, CharacteristicIntValue>();
            CreateMap<CharacteristicValueDto, CharacteristicBoolValue>();

            CreateMap<CharacteristicTypeDto, CharacteristicType>();
        }
    }
}
