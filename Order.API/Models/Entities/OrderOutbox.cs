namespace Order.API.Models.Entities
{
	public class OrderOutbox
	{
		public int Id { get; set; }
		public DateTime OccuredOn { get; set; }
		public DateTime? ProcessDate { get; set; }
		public string Type { get; set; }
		public string Payload { get; set; }
	}
}
