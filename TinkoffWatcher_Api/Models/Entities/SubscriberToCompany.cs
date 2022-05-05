using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class SubscriberToCompany
    {
        public Guid Id { get; set; }

        public Guid SubscriberId { get; set; }
        [InverseProperty(nameof(ApplicationUser.Subscriptions))]
        public virtual ApplicationUser Subscriber { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
