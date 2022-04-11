using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Responses;
using TinkoffWatcher_Api.Services;

namespace TinkoffWatcher_Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly INotificationsService _notificationsService;

		public NotificationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, INotificationsService notificationsService)
		{
			_context = context;
			_userManager = userManager;
			_notificationsService = notificationsService;
		}

		[Authorize]
		[HttpGet]
		[Route("get_fcm_token")]
		public async Task<ActionResult<FCMTokenResponse>> GetFCMToken()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			return Ok(new FCMTokenResponse { Token = user.FCMToken });
		}

		[Authorize]
		[HttpPatch]
		[Route("add_fcm_token")]
		public async Task<ActionResult<Response>> AddFCMToken([FromBody] string token)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			user.FCMToken = token;
			await _context.SaveChangesAsync();

			_notificationsService.SendNotificationAsync(token, "Настройки обновлены!", "Теперь вы будете получать уведомления на этом устройстве.");

			return Ok(new Response { Status = "Success", Message = "Token added successfully!" });
		}

		[Authorize]
		[HttpDelete]
		[Route("delete_fcm_token")]
		public async Task<ActionResult<Response>> DeleteFCMToken()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			user.FCMToken = null;
			await _context.SaveChangesAsync();
			return Ok(new Response { Status = "Success", Message = "Token removed successfully!" });
		}

#nullable enable
		[Authorize]
		[HttpPost]
		[Route("send_message")]
		public async Task<ActionResult<Response>> SendMessage([FromBody] SendMessageModel model)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			string? fcmToken = user.FCMToken;
			if (fcmToken is not null and not "")
			{
				_notificationsService.SendNotificationAsync(fcmToken, model.Title, model.Message);
			}

			return Ok(new Response { Status = "Success", Message = "Message sent!" });
		}
	}
}
