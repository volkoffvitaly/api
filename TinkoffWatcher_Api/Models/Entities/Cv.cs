using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Cv : BaseEntity
    {
        public string AboutMe { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<LanguageProficiency> LanguageProficiencies { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
        public virtual ICollection<UsefulLink> UsefulLinks { get; set; }
    }
}
