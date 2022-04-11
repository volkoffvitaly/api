using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinkoff.Trading.OpenApi.Models
{
	public class Portfolio
	{
		public List<TPosition> Positions { get; }

		[JsonConstructor]
		public Portfolio(List<TPosition> positions)
		{
			Positions = positions;
		}

		public class TPosition
		{
			public string Name { get; set; }
			public string Figi { get; set; }
			public string Ticker { get; set; }
			public string Isin { get; set; }
			public InstrumentType InstrumentType { get; set; }
			public decimal Balance { get; set; }
			public decimal Blocked { get; set; }
			public MoneyAmount ExpectedYield { get; }
			public int Lots { get; set; }
			public MoneyAmount AveragePositionPrice { get; set; }
			public MoneyAmount AveragePositionPriceNoNkd { get; }

			[JsonConstructor]
			public TPosition(
				string name,
				string figi,
				string ticker,
				string isin,
				InstrumentType instrumentType,
				decimal balance,
				decimal blocked,
				MoneyAmount expectedYield,
				int lots,
				MoneyAmount averagePositionPrice,
				MoneyAmount averagePositionPriceNoNkd)
			{
				Name = name;
				Figi = figi;
				Ticker = ticker;
				Isin = isin;
				InstrumentType = instrumentType;
				Balance = balance;
				Blocked = blocked;
				ExpectedYield = expectedYield;
				Lots = lots;
				AveragePositionPrice = averagePositionPrice;
				AveragePositionPriceNoNkd = averagePositionPriceNoNkd;
			}

			public TPosition() { }
		}
	}
}
