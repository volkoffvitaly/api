using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TinkoffWatcher_Api.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string TinkoffToken { get; set; }
		public string FCMToken { get; set; }

		public ICollection<PositionSettings> PositionsSettings { get; set; }
		public ICollection<UserPosition> Positions { get; set; }
		public ICollection<Subscription> Subscriptions { get; set; }
	}
}
