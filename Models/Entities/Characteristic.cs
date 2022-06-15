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
        
        public Guid? CharacteristicQuestionId { get; set; }
        public virtual CharacteristicQuestion CharacteristicQuestion { get; set; }
        public virtual ICollection<CharacteristicAnswer> CharacteristicAnswers { get; set; }
        public Guid? MarkId { get; set; }
        public virtual Mark Mark { get; set; }
    }
}
