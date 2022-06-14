using System;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class MarkCharacteristicEditDto 
    {
        public string Other { get; set; }

        public Guid CharacteristicTypeId { get; set; }
        public Guid CharacteristicValueId { get; set; }
    }
}
