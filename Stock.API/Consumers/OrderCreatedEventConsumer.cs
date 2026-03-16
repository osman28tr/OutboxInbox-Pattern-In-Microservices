using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events.Abstract;
using Stock.API.Models;
using Stock.API.Models.Entities;
using System.Text.Json;

namespace Stock.API.Consumers
{
	public class OrderCreatedEventConsumer(AppDbContext _context) : IConsumer<IOrderCreatedEvent>
	{
		public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
		{
			await _context.OrderInboxs.AddAsync(new OrderInbox
			{
				IsProcessed = false,
				Payload = JsonSerializer.Serialize(context.Message)
			});

			await _context.SaveChangesAsync();

			List<OrderInbox> orderInboxes = await _context.OrderInboxs.Where(x => x.IsProcessed == false).ToListAsync();

			foreach (var orderInbox in orderInboxes)
			{
				OrderCreatedEvent orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderInbox.Payload);
				await Console.Out.WriteLineAsync($"{orderCreatedEvent.OrderId}");
				orderInbox.IsProcessed = true;
			}
			await _context.SaveChangesAsync();
		}
	}
}
