using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class CharacteristicAnswer : BaseVersionedEntity
    {
        public string Description { get; set; }
        
        public Guid CharacteristicQuestionId { get; set; }
        public virtual CharacteristicQuestion CharacteristicQuestion { get; set; }
        public virtual ICollection<Characteristic> Characteristics { get; set; }
    }
}
