using Order.API.Models;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.Abstract
{
	public interface IOrderCreatedEvent
	{
		public Guid IdempotentToken { get; set; }
		public int OrderId { get; set; }
		public string UserId { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<OrderCreatedMessage> OrderItems { get; set; }
	}
}
