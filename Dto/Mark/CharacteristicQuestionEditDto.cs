using System;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicQuestionEditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CharacteristicAnswerEditDto> CharacteristicAnswers { get; set; }
    }
}
