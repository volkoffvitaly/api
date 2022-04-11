using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffWatcher_Api.Models
{
	public class EditPositionSettingsModel
	{
		public string Figi { get; set; }
		public bool? IsTrailStopEnabledByUser { get; set; } = null;
		public bool? IsObserveEnabled { get; set; } = null;
		public decimal? TakeProfitPrice { get; set; } = null;
		public double? StopLossPercent { get; set; } = null;
		public OrderType? OrderType { get; set; } = null;
	}
}
