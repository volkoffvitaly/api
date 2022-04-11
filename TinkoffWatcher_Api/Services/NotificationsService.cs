using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Models;

namespace TinkoffWatcher_Api.Services
{
	public class NotificationsService : INotificationsService
	{
		private readonly string serverKey = "";
		private readonly string senderId = "";

		public async Task<bool> SendNotificationAsync(string DeviceToken, string title, string msg)
		{
			using HttpClient client = new();
			client.BaseAddress = new Uri("https://fcm.googleapis.com");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_ = client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
				$"key={serverKey}");
			_ = client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={senderId}");

			var data = new
			{
				to = DeviceToken,
				data = new MessagePayload
				{
					Title = title,
					Message = msg
				}
			};

			string json = JsonSerializer.Serialize(data);
			StringContent httpContent = new(json, Encoding.UTF8, "application/json");

			HttpResponseMessage result = await client.PostAsync("/fcm/send", httpContent);
			return result.IsSuccessStatusCode;
		}
	}
}
