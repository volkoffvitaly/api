using Models;
using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Slot : BaseEntity
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        /// <summary>
        /// В минутах
        /// </summary>
        public int Duration { get; set; }

        public Guid? StudentId { get; set; }
        public virtual ApplicationUser Student { get; set; }

        public Guid VacancyId { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}