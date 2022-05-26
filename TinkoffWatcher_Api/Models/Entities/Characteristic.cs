using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Characteristic : BaseEntity
    {
        public string Other { get; set; }

        public Guid? CharacteristicTypeId { get; set; }
        public virtual CharacteristicType CharacteristicType { get; set; }
        public Guid? CharacteristicValueId { get; set; }
        public virtual CharacteristicValue CharacteristicValue { get; set; }
        public Guid? MarkId { get; set; }
        public virtual Mark Mark { get; set; }
    }
}
