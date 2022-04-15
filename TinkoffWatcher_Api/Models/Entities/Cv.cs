using Models;
using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Cv : BaseEntity
    {
        public string AboutMe { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<LanguageProficiency> LanguageProficiencies { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        public ICollection<UsefulLink> UsefulLinks { get; set; }
    }
}
