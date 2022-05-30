using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicDto : BaseDto
    {
        public string Other { get; set; }

        public virtual CharacteristicTypeDto CharacteristicType { get; set; }
        public virtual CharacteristicValueDto CharacteristicValue { get; set; }
    }
}
