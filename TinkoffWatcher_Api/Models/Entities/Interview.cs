using Models;
using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Interview : BaseEntity
    {
        public DateTime Date { get; set; }
        public string AdditionalInfo { get; set; }

        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public ICollection<ApplicationUser> Agents { get; set; }
        public Guid VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
