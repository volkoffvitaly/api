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
                .ForMember(x => x.Cv, opt => opt.MapFrom(x => x.Cv))
                .ForMember(x => x.Company, opt => opt.MapFrom(x => x.Company))
                .ForMember(x => x.CompanySubscriptions, opt => opt.MapFrom(x => x.Subscriptions.Select(y => y.Company)))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => _userManager.GetRolesAsync(x).GetAwaiter().GetResult()));

            CreateMap<CompanyEditDto, Company>();
            CreateMap<Company, CompanyDto>();

            CreateMap<FeedbackEditDto, Feedback>();
            CreateMap<FeedbackCreateDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>();

            CreateMap<InterviewCreateOrEditDto, Interview>();
            CreateMap<Interview, InterviewDto>()
                .ForMember(x => x.Feedbacks, opt => opt.MapFrom(x => x.Feedbacks));

            CreateMap<VacancyEditDto, Vacancy>();
            CreateMap<Vacancy, VacancyDto>();

            CreateMap<SlotEditDto, Slot>();
            CreateMap<Slot, DiarySettingsDto>();

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

            CreateMap<CharacteristicAnswer, CharacteristicAnswerDto>();

            CreateMap<CharacteristicQuestion, CharacteristicQuestionDto>()
                .ForMember(_ => _.CharacteristicAnswers, opt => opt.MapFrom(x => x.CharacteristicAnswers));

            CreateMap<Characteristic, CharacteristicDto>()
                .ForMember(_ => _.CharacteristicAnswers, opt => opt.MapFrom(x => x.CharacteristicAnswers))
                .ForMember(_ => _.CharacteristicQuestions, opt => opt.MapFrom(x => x.CharacteristicQuestion));

            CreateMap<MarkCharacteristicEditDto, Characteristic>()
                .ForMember(_ => _.CharacteristicAnswers, opt => opt.Ignore());

            CreateMap<MarkEditDto, Mark>();
            CreateMap<Mark, MarkDto>()
                .ForMember(x => x.Characteristics, opt => opt.MapFrom(x => x.Characteristics));

            CreateMap<DiarySettingsEditDto, DiarySettings>();
            CreateMap<DiarySettings, DiarySettingsDto>();

            CreateMap<CharacteristicQuestionDto, CharacteristicQuestion>();

        }
    }
}
