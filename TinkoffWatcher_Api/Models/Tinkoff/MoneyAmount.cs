using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tinkoff.Trading.OpenApi.Models
{
	[Owned]
	public class MoneyAmount
	{
		public Currency Currency { get; set; }
		[Column(TypeName = "decimal(18,5)")]
		public decimal Value { get; set; }

		[JsonConstructor]
		public MoneyAmount(Currency currency, decimal value)
		{
			Currency = currency;
			Value = value;
		}

		public MoneyAmount()
		{
		}
	}
}