using System.Collections.Generic;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicTypeCreateDto
    {
        public string Name { get; set; }
        public ICollection<CharacteristicValueCreateDto>? CharacteristicValues { get; set; }
    }
}
