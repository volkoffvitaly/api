using Models;
using System;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class WorkExperience : BaseEntity
    {
        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string WorkResponsibilities { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid CvId { get; set; }
        public Cv Cv { get; set; }
    }
}
