using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Dto.Vacancy
{
    public class VacancyEditDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
        [IntegerValidator(MinValue = 1, MaxValue = 100)]
        public int PositionAmount { get; set; }

        public Guid CompanyId { get; set; }
    }
}
