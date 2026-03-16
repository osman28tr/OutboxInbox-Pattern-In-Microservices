using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.Models
{
	public class CreateOrderDto
	{
		public string UserId { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<OrderItemDto> Items { get; set; }
	}
	public class OrderItemDto
	{
		public int ProductId { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		public int Count { get; set; }
	}
}
