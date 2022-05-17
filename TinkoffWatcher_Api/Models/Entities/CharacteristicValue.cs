using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models.Entities
{
    public abstract class CharacteristicValue : BaseVersionedEntity
    {
        public string Description { get; set; }
        
        public Guid CharacteristicTypeId { get; set; }
        public virtual CharacteristicType CharacteristicType { get; set; }
    }
}
