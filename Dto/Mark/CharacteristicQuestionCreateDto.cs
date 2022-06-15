using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicQuestionCreateDto
    {
        public string Name { get; set; }
        public bool IsMultipleValues { get; set; }
        public ICollection<CharacteristicAnswerCreateDto> CharacteristicAnswers { get; set; }
    }
}
