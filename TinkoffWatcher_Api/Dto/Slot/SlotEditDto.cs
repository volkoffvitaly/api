using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Slot
{
    public class SlotEditDto
    {
        public DateTime DateTime { get; set; }

        /// <summary>
        /// В минутах
        /// </summary>
        public int Duration { get; set; }

        public Guid? StudentId { get; set; }
        public Guid VacancyId { get; set; }
    }
}
