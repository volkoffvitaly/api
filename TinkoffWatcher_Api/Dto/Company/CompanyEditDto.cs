using System.ComponentModel.DataAnnotations;

namespace TinkoffWatcher_Api.Dto.Company
{
    public class CompanyEditDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
