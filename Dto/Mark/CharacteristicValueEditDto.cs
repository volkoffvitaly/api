using System;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicValueEditDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int? IntValue { get; set; }
        public bool? BoolValue { get; set; }
    }
}