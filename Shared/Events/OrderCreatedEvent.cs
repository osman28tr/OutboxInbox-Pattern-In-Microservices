using Shared.Events.Abstract;
using Shared.Messages;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.Models
{
	public class OrderCreatedEvent : IOrderCreatedEvent
	{
		public int OrderId { get; set; }
		public string UserId { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<OrderCreatedMessage> OrderItems { get ; set; }
	}
}
