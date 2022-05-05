using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Dto.Interview
{
    public class InterviewEditDto 
    {
        public DateTime Date { get; set; }
        [MaxLength(255)]
        public string AdditionalInfo { get; set; }

        public Guid StudentId { get; set; }
        public Guid VacancyId { get; set; }
    }
}
