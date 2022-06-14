using Models;
using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Interview : BaseEntity
    {
        public DateTime Date { get; set; }
        public string AdditionalInfo { get; set; }
        /// <summary>
        /// В минутах
        /// </summary>
        public int Duration { get; set; }

        public Guid? StudentId { get; set; }
        public virtual ApplicationUser Student { get; set; }
        public virtual ICollection<ApplicationUser> Agents { get; set; }
        public Guid VacancyId { get; set; }
        public virtual Vacancy Vacancy { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }


    }
}
