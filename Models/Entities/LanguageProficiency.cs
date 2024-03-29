﻿using Models;
using System;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class LanguageProficiency : BaseEntity
    {
        public LanguageLevelEnum LanguageLevel { get; set; }

        public Guid LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public Guid CvId { get; set; }
        public virtual Cv Cv { get; set; }
    }
}
