using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.Cv
{
    public class LanguageProficiencyDto 
    {
        public Guid Id { get; set; }

        public LanguageLevelEnum LanguageLevel { get; set; }

        public LanguageDto Language { get; set; }
    }
}
