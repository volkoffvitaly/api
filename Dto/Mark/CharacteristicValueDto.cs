using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicValueDto : BaseDto
    {
        public string Description { get; set; }
        public int? IntValue { get; set; }
        public bool? BoolValue { get; set; }
    }
}
