using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Feedback
{
    public class MarkEditDto 
    {
        [Range(1, 5)]
        public int Value { get; set; }
        public string Review { get; set; }

        [Range(2018, 9999)]
        public int Year { get; set; }
        public SemesterEnum Semester { get; set; }

        public Guid StudentId { get; set; }
        public Guid AgentId { get; set; }
    }
}
