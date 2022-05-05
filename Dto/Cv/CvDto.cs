using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Dto.Cv
{
    public class CvDto : BaseDto
    {
        public string AboutMe { get; set; }
        public Guid UserId { get; set; }

        public ICollection<LanguageProficiencyDto> LanguageProficiencies { get; set; }
        public ICollection<WorkExperienceDto> WorkExperiences { get; set; }
        public ICollection<UsefulLinkDto> UsefulLinks { get; set; }
    }
}
