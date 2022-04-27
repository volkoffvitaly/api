using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TinkoffWatcher_Api.Dto.Company;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Dto.Interview;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Dto.Vacancy;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, FullUserInfoDto>();

            CreateMap<CompanyEditDto, Company>();
            CreateMap<Company, CompanyDto>();

            CreateMap<FeedbackEditDto, Feedback>();
            CreateMap<FeedbackCreateDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>();

            CreateMap<InterviewEditDto, Interview>();
            CreateMap<Interview, InterviewDto>();

            CreateMap<VacancyEditDto, Vacancy>();
            CreateMap<Vacancy, VacancyDto>();
        }
    }
}
