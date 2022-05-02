using System;
using System.Collections.Generic;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Models;

namespace TinkoffWatcher_Api.Dto.Interview
{
    public class InterviewDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string AdditionalInfo { get; set; }

        public Guid StudentId { get; set; }
        public FullUserInfoDto Student { get; set; }
        public ICollection<FullUserInfoDto> Agents { get; set; }
        public Guid VacancyId { get; set; }
        public ICollection<Models.Entities.Feedback> Feedbacks { get; set; }
    }
}