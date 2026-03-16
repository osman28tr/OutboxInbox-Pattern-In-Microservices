using System.Net;

namespace Order.API.Models.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<OrderItem> Items { get; set; }
	}
}
