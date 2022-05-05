using System;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Filters
{
    /// <summary>
    /// Фильтр для оценок
    /// </summary>
    public class SlotsFilter
    {
        public DateTime? StartDate { get; set; }
        public Guid? VacancyId { get; set; }
    }
}
