using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Services
{
	public interface INotificationsService
	{
		public Task<bool> SendNotificationAsync(string DeviceToken, string title, string msg);
	}
}
