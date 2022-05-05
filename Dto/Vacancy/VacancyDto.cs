using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Vacancy
{
    public class VacancyDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PositionAmount { get; set; }
        public Guid CompanyId { get; set; }
    }
}
