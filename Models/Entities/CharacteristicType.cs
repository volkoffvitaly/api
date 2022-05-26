using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class CharacteristicType : BaseVersionedEntity
    {
        public string Name { get; set; }
        public virtual ICollection<CharacteristicValue> CharacteristicValues { get; set; }
    }
}
