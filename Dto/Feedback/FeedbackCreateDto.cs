﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Feedback
{
    public class FeedbackCreateDto
    {
        public string Text { get; set; }
        public VerdictEnum Verdict { get; set; }

        public Guid InterviewId { get; set; }
    }
}
