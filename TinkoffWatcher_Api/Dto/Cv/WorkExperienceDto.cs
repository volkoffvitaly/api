using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Cv
{
    public class WorkExperienceDto 
    {
        public Guid Id { get; set; }
        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string WorkResponsibilities { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
