using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Filters
{
    /// <summary>
    /// Фильтр для слотов
    /// </summary>
    public class MarksFilter
    {
        public string? Value { get; set; }
        public int? StartYear { get; set; }

        public int? EndYear { get; set; }
        public SemesterEnum? Semester { get; set; }
        public Grade? Grade { get; set; }
    }
}
