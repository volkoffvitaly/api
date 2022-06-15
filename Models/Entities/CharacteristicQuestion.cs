using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class CharacteristicQuestion : BaseVersionedEntity
    {
        public string Name { get; set; }
        public bool IsMultipleValues { get; set; }
        public virtual ICollection<CharacteristicAnswer> CharacteristicAnswers { get; set; }
    }
}
