using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Filters
{
    /// <summary>
    /// Фильтр для оценок
    /// </summary>
    public class MarksFilter //: BaseFilter
    {
        //public EmployeeRequestFilter()
        //{
        //    DefaultSortColumn = "RequestType";
        //    DefaultSortDesc = false;
        //}

        public int? Value { get; set; }
        public int? StartYear { get; set; }

        public int? EndYear { get; set; }
        public SemesterEnum? Semester { get; set; }
    }
}
