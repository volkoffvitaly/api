using System.Text.Json.Serialization;

namespace TinkoffWatcher_Api.Models
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum PositionType
	{
		Short,
		Long
	}
}
