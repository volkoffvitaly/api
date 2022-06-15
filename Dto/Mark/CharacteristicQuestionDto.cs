using System.Collections.Generic;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicQuestionDto : BaseDto
    {
        public string Name { get; set; }
        public virtual ICollection<CharacteristicAnswerDto> CharacteristicAnswers { get; set; }
    }
}
