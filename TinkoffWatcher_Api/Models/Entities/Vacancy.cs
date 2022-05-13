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
        public virtual Company Company { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
}
}