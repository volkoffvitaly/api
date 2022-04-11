
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffWatcher_Api.Models
{
	public class UpdatePositionInfoModel
	{
		public string Figi { get; set; }
		public decimal? MaxPrice { get; set; }
		public decimal Balance { get; set; }
		public int Lots { get; set; }
		public MoneyAmount AveragePositionPrice { get; set; }
		public decimal LastPrice { get; set; }
		public PositionType PositionType { get; set; }
	}
}
