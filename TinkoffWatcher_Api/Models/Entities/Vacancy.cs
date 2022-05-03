using Models;
using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Vacancy : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PositionAmount { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Interview> Interviews { get; set; }
        public ICollection<Slot> Slots { get; set; }
}
}