using MassTransit;
using Order.API.Models;
using Order.Outbox.Table.Publisher.Service.Entities;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Outbox.Table.Publisher.Service.Jobs
{
	public class OrderOutboxPublishJob(IPublishEndpoint _publishEndpoint) : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			if (OrderOutboxPublisherContext.DataReaderState())
			{
				OrderOutboxPublisherContext.DataReaderBusy();

				List<OrderOutbox> orderOutboxes = (await OrderOutboxPublisherContext.QueryAsync<OrderOutbox>($@"select * from OrderOutboxes where " +
					$@"ProcessDate IS NULL ORDER BY OccuredOn ASC")).ToList();

				if (orderOutboxes.Count > 0)
				{
					foreach (var orderOutbox in orderOutboxes)
					{
						if (orderOutbox.Type == nameof(OrderCreatedEvent))
						{
							OrderCreatedEvent orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderOutbox.Payload);

							if (orderCreatedEvent != null)
							{
								await _publishEndpoint.Publish(orderCreatedEvent);

								await OrderOutboxPublisherContext.ExecuteAsync($"update OrderOutboxes set ProcessDate = getdate() " +
									$"where Id = {orderOutbox.Id}");
							}
						}
					}
				}

				OrderOutboxPublisherContext.DataReaderReady();
				await Console.Out.WriteLineAsync("Order outbox table checked!");
			}
		}
	}
}
