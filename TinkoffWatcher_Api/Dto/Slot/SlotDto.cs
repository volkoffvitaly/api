﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Slot
{
    public class SlotDto : BaseDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        /// <summary>
        /// В минутах
        /// </summary>
        public int Duration { get; set; }

        public Guid? StudentId { get; set; }
        public Guid VacancyId { get; set; }
    }
}