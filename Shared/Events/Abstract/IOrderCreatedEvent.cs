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
		public List<OrderCreatedMessage> OrderItems { get; set; }
	}
}
