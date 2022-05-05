using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Dto.Cv
{
    public class CvCreateDto
    {
        public string AboutMe { get; set; }
        public Guid UserId { get; set; }

        public ICollection<LanguageProficiencyCreateDto> LanguageProficiencies { get; set; }
        public ICollection<WorkExperienceCreateDto> WorkExperiences { get; set; }
        public ICollection<UsefulLinkCreateDto> UsefulLinks { get; set; }
    }
}
