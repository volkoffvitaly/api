using System;

namespace TinkoffWatcher_Api.Models.Responses
{
	public class LoginResponse
	{
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
