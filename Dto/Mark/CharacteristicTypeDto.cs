using System.Collections.Generic;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicTypeDto : BaseDto
    {
        public string Name { get; set; }
        public virtual ICollection<CharacteristicValueDto> CharacteristicValues { get; set; }
    }
}
