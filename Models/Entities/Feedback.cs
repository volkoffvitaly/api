﻿using Models;
using System;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Feedback : BaseEntity
    {
        public Guid InterviewId { get; set; }
        public virtual Interview Interview { get; set; }
        public string Text { get; set; }
        public VerdictEnum Verdict { get; set; }
    }
}