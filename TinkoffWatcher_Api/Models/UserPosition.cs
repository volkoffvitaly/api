namespace TinkoffWatcher_Api.Models
{
	public class UserPosition
	{
		public string OwnerId { get; set; }
		public ApplicationUser Owner { get; set; }

		public string PositionFigi { get; set; }
		public Position Position { get; set; }
	}
}
