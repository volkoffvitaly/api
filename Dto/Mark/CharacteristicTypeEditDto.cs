using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicTypeEditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CharacteristicValueEditDto>? CharacteristicValues { get; set; }
    }
}
