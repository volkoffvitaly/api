using System;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicEditDto 
    {
        public string Other { get; set; }

        public virtual CharacteristicQuestionEditDto CharacteristicQuestions { get; set; }
        public virtual CharacteristicAnswerEditDto CharacteristicAnswers { get; set; }
    }
}
