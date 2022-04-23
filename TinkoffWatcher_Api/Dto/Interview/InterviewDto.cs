using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Dto.Interview
{
    public class InterviewDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string AdditionalInfo { get; set; }

        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public ICollection<ApplicationUser> Agents { get; set; }
        public Guid VacancyId { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
