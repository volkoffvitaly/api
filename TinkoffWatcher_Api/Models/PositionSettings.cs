using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffWatcher_Api.Models
{
	public class PositionSettings
	{
		public Guid Id { get; set; }

		public string PositionFigi { get; set; }
		public Position Position { get; set; }

		public string OwnerId { get; set; }
		public ApplicationUser Owner { get; set; }

		public decimal Balance { get; set; }
		public int Lots { get; set; }
		public MoneyAmount AveragePositionPrice { get; set; }

		[Column(TypeName = "decimal(18,5)")]
		public decimal? MaxPrice { get; set; } = null;
		public bool IsTrailStopEnabledByUser { get; set; } = false;
		public bool IsObserveEnabled { get; set; } = true;
		[Column(TypeName = "decimal(18,5)")]
		public decimal? ActivationPrice { get; set; } = null;
		public double? StopLossPercent { get; set; } = null;
		public OrderType? OrderType { get; set; } = null;
		public PositionType PositionType { get; set; }
	}
}
