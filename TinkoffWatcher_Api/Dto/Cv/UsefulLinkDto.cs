using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Dto.Base;

namespace TinkoffWatcher_Api.Dto.Cv
{
    public class UsefulLinkDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
