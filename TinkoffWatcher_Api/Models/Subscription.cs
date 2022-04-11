using System;

namespace TinkoffWatcher_Api.Models
{
	public class Subscription
	{
		public Guid Id { get; set; }

		public string OwnerId { get; set; }
		public ApplicationUser Owner { get; set; }

		public DateTime SubscriptionExpiration { get; set; }
	}
}
