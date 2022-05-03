using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Dto.User;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Feedback
{
    public class MarkDto : BaseDto
    {
        public int Value { get; set; }
        public string Review { get; set; }

        public int Year { get; set; }
        public SemesterEnum Semester { get; set; }

        public Guid StudentId { get; set; }
        public FullUserInfoDto Student { get; set; }
        public Guid AgentId { get; set; }
        public FullUserInfoDto Agent { get; set; }
    }
}
