using Models;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Employees { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
