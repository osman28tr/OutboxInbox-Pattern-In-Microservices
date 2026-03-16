namespace Stock.API.Models.Entities
{
	public class OrderInbox
	{
		public int Id { get; set; }
		public bool IsProcessed { get; set; }
		public string Payload { get; set; }
	}
}
