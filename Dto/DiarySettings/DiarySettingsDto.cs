﻿using System;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Slot
{
    public class DiarySettingsDto : BaseDto
    {
        public Grade Grade { get; set; }
        public string Order { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
