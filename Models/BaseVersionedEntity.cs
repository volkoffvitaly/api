using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models
{
    public class BaseVersionedEntity : BaseEntity, ICloneable
    {
        public bool IsDeleted { get; set; }
        public int Version { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime VersionDate { get; set; }

        public BaseVersionedEntity()
        {
            Version = 1;
            IsCurrent = true;
            VersionDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        public object Clone()
        {
            IsCurrent = false;
            var newVersion = (BaseVersionedEntity)this.MemberwiseClone();
            newVersion.Version++;
            newVersion.IsCurrent = true;
            newVersion.VersionDate = DateTime.UtcNow;
            return newVersion;
        }
    }
}
