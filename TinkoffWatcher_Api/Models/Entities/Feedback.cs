using Models;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Feedback : BaseEntity
    {
        public string Text { get; set; }
        public VerdictEnum Verdict { get; set; }
    }
}
