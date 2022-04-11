using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;
using TinkoffWatcher_Api.Data;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Responses;

namespace TinkoffWatcher_Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class StocksController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> userManager;

		public StocksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			this.userManager = userManager;
		}

		[Authorize]
		[HttpPatch]
		[Route("update_positions_list")]
		public async Task<IActionResult> UpdatePositionsList([FromBody] List<Portfolio.TPosition> positions)
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			try
			{
				await _context.Entry(user).Collection(x => x.Positions).LoadAsync();
				await _context.Entry(user).Collection(x => x.PositionsSettings).LoadAsync();

				var existedPositions = user.Positions;
				var positionsToAdd = positions.Where(x => !existedPositions.Any(p => p.PositionFigi == x.Figi));

				var userPositionsSettings = user.PositionsSettings;
				var positionsToDelete = userPositionsSettings.Where(x => !positions.Any(p => p.Figi == x.PositionFigi));
				var userPositionsToDelete = _context.UsersPositions.ToList().Where(x => x.OwnerId == user.Id && positionsToDelete.Any(p => p.PositionFigi == x.PositionFigi));

				foreach (var position in positionsToAdd)
				{
					var newPosition = new Position { Isin = position.Isin, Figi = position.Figi, InstrumentType = position.InstrumentType, Name = position.Name, Ticker = position.Ticker };
					var newUserPosition = new UserPosition { OwnerId = user.Id, PositionFigi = newPosition.Figi };

					var newPositionSettings = new PositionSettings { PositionFigi = newPosition.Figi, OwnerId = user.Id };

					if (_context.Positions.SingleOrDefault(x => x.Figi == newPosition.Figi) == default)
					{
						_context.Positions.Add(newPosition);
					}

					_context.UsersPositions.Add(newUserPosition);
					_context.PositionsSettings.Add(newPositionSettings);
				}

				/*Parallel.ForEach(positionsToAdd, position =>
                {
                    var newPosition = new Position { Isin = position.Isin, Figi = position.Figi, InstrumentType = position.InstrumentType, Name = position.Name, Ticker = position.Ticker };
                    var newUserPosition = new UserPosition { OwnerId = user.Id, PositionFigi = newPosition.Figi };

                    var newPositionSettings = new PositionSettings { PositionFigi = newPosition.Figi, OwnerId = user.Id };

                    if (_context.Positions.SingleOrDefault(x => x.Figi == newPosition.Figi) != default)
                    {
                        _context.Positions.Add(newPosition);
                    }

                    _context.UsersPositions.Add(newUserPosition);
                    _context.PositionsSettings.Add(newPositionSettings);
                });*/

				_context.PositionsSettings.RemoveRange(positionsToDelete);
				_context.UsersPositions.RemoveRange(userPositionsToDelete);

				await _context.SaveChangesAsync();

				return Ok();
			}
			catch (Exception e)
			{
				// showing log info to user - BAAAAD IDEA
				return StatusCode(500, new Response { Status = "Database update error.", Message = e.Message });
			}
		}

		[Authorize]
		[HttpPut]
		[Route("update_position_info")]
		public async Task<IActionResult> UpdatePositionInfo([FromBody] UpdatePositionInfoModel model)
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			try
			{
				//TODO: better think about include instead of individual last price
				var existedPosition = _context.PositionsSettings.Include(x => x.Position).Single(x => x.OwnerId == user.Id && x.PositionFigi == model.Figi);
				existedPosition.MaxPrice = model.MaxPrice;
				existedPosition.Balance = model.Balance;
				existedPosition.Lots = model.Lots;
				existedPosition.AveragePositionPrice = model.AveragePositionPrice;
				existedPosition.Position.LastPrice = model.LastPrice;
				existedPosition.PositionType = model.PositionType;

				await _context.SaveChangesAsync();

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, new Response { Status = "Database update error.", Message = e.Message });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("edit_position_settings")]
		public async Task<IActionResult> EditStockSettings([FromBody] EditPositionSettingsModel model)
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			try
			{
				var existedPosition = _context.PositionsSettings.Single(x => x.OwnerId == user.Id && x.PositionFigi == model.Figi);

				if (model.IsTrailStopEnabledByUser != null)
				{
					if (existedPosition.IsTrailStopEnabledByUser != model.IsTrailStopEnabledByUser && (bool)model.IsTrailStopEnabledByUser) //need to set max price value to last price in case of the new cycle of trailing stop
					{
						await _context.Entry(existedPosition).Reference(x => x.Position).LoadAsync();
						existedPosition.MaxPrice = existedPosition.Position.LastPrice;
					}
					existedPosition.IsTrailStopEnabledByUser = (bool)model.IsTrailStopEnabledByUser;
				}

				if (model.IsObserveEnabled != null)
				{
					existedPosition.IsObserveEnabled = (bool)model.IsObserveEnabled;
				}

				if (model.TakeProfitPrice != null)
				{
					existedPosition.ActivationPrice = (decimal)model.TakeProfitPrice;
				}

				if (model.StopLossPercent != null)
				{
					existedPosition.StopLossPercent = (double)model.StopLossPercent;
				}

				if (model.OrderType != null)
				{
					existedPosition.OrderType = (OrderType)model.OrderType;
				}

				await _context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, new Response { Status = "Database update error.", Message = e.Message });
			}
		}

		[Authorize]
		[HttpGet]
		[Route("get_user_positions")]
		public async Task<ActionResult<List<PositionSettings>>> GetUserPositions()
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return Unauthorized();
			}

			try
			{
				var positions = _context.PositionsSettings.Include(x => x.Position).Where(x => x.OwnerId == user.Id);

				Parallel.ForEach(positions, position =>
				{
					position.Position.PositionSettings = null;
					position.OwnerId = null;
					position.Owner = null;
				});

				return Ok(positions);
			}
			catch (Exception e)
			{
				return StatusCode(500);
			}
		}
	}
}
