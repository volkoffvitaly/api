using System;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicEditDto 
    {
        public string Other { get; set; }

        public virtual CharacteristicTypeEditDto CharacteristicType { get; set; }
        public virtual CharacteristicValueEditDto CharacteristicValue { get; set; }
    }
}
