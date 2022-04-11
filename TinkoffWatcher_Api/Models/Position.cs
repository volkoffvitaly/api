using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffWatcher_Api.Models
{
	public class Position
	{
		public string Name { get; set; }
		[Key]
		public string Figi { get; set; }
		public string Ticker { get; set; }
		public string Isin { get; set; }
		public InstrumentType InstrumentType { get; set; }

		[Column(TypeName = "decimal(18,5)")]
		public decimal LastPrice { get; set; }

		public ICollection<PositionSettings> PositionSettings { get; set; }
		public ICollection<UserPosition> Users { get; set; }
	}
}
