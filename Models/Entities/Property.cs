using Models;

namespace TinkoffWatcher_Api.Models.Entities
{   
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
