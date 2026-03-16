using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events.Abstract;
using Stock.API.Models;
using System.Text.Json;

namespace Stock.API.Consumers
{
	public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
	{
		public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
		{
			await Console.Out.WriteLineAsync(JsonSerializer.Serialize(context.Message));
		}
	}
}
