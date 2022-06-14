using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TinkoffWatcher_Api.Dto.Mark;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Feedback
{
    public class MarkEditDto 
    {
        public SemesterEnum Semester { get; set; }

        public Guid StudentId { get; set; }
        public Guid AgentId { get; set; }

        public string OverallMark { get; set; }
        public string AdditionalComment { get; set; }

        [Range(2018, 9999)]
        public int Year { get; set; }

        public virtual ICollection<MarkCharacteristicEditDto> Characteristics { get; set; }
    }
}
