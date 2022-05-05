using System;

namespace TinkoffWatcher_Api.Dto.Base
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? EditedDate { get; set; }
    }
}
