using System;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Filters
{
    /// <summary>
    /// Фильтр для оценок
    /// </summary>
    public class SlotsFilter
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public Guid? VacancyId { get; set; }
    }
}
