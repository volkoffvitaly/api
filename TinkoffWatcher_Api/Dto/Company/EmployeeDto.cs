using System;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Company
{
    public class EmployeeDto : BaseDto
    {
        public Guid UserId { get; set; }
    }
}
