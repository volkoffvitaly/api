using Models;
using System;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class UsefulLink : BaseEntity
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public Guid CvId { get; set; }
        public Cv Cv { get; set; }
    }
}
