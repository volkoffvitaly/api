using System;
using System.Collections.Generic;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Mark
{
    public class MarkCharacteristicEditDto 
    {
        public string Other { get; set; }

        public Guid CharacteristicQuestionId { get; set; }
        public List<Guid> CharacteristicAnswerIds { get; set; }
    }
}
