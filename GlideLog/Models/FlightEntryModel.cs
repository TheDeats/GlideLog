using SQLite;

namespace GlideLog.Models
{
	public class FlightEntryModel
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public DateTime DateNTime { get; set; }
		public string Site {  get; set; } = string.Empty;
		public string Glider { get; set; } = string.Empty;
		public int FlightCount { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public bool OmitFromTotals { get; set; }
		public string Notes { get; set; } = string.Empty;
	}
}
