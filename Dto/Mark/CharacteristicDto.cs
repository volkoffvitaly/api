using System.Collections.Generic;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicDto : BaseDto
    {
        public string Other { get; set; }

        public virtual CharacteristicQuestionDto CharacteristicQuestions { get; set; }
        public virtual List<CharacteristicAnswerDto> CharacteristicAnswers { get; set; }
    }
}
