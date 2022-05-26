using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class CharacteristicBoolValue : CharacteristicValue
    {
        public bool? BoolValue { get; set; }
    }
}
