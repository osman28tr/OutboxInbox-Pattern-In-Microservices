using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API;
using Order.API.Models.Entities;
using Order.API.Models;
using Shared;
using Shared.Events.Abstract;
using Shared.Messages;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDBMSSQL")));

builder.Services.AddMassTransit(x =>
{	
	x.UsingRabbitMq((_context, _configurator) =>
	{
		_configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"));		
	});
});

var app = builder.Build();
app.MapPost("/create-order",async (CreateOrderDto createOrderDto,ISendEndpointProvider sendEndPointProvider,AppDbContext dbContext) =>{
	var order = new Order.API.Models.Entities.Order()
	{
		CreatedDate = DateTime.Now,
		UserId = createOrderDto.UserId,
		TotalPrice = createOrderDto.Items.Sum(x => x.Price * x.Count),
		Items = createOrderDto.Items.Select(oi => new OrderItem()
		{
			Count = oi.Count,
			Price = oi.Price,
			ProductId = oi.ProductId,
		}).ToList()
	};

	await dbContext.Orders.AddAsync(order);
	await dbContext.SaveChangesAsync();

	OrderCreatedEvent orderCreatedEvent = new OrderCreatedEvent()
	{
		CreatedDate = DateTime.UtcNow,
		TotalPrice = order.TotalPrice,
		OrderId = order.Id,
		UserId = order.UserId,
		OrderItems = order.Items.Select(oi => new OrderCreatedMessage()
		{
			Count = oi.Count,
			Price = oi.Price,
			ProductId = oi.ProductId,
		}).ToList()
	};

	#region without outbox pattern	
	//var sendEndPoint = await sendEndPointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.Stock_OrderCreatedEvent}"));
	//await sendEndPoint.Send<IOrderCreatedEvent>(orderCreatedEvent);
	#endregion

	#region with outbox pattern
	OrderOutbox orderOutbox = new()
	{
		OccuredOn = DateTime.Now,
		ProcessDate = null,
		Payload = JsonSerializer.Serialize(orderCreatedEvent),
		Type = orderCreatedEvent.GetType().Name
	};
	await dbContext.OrderOutboxes.AddAsync(orderOutbox);
	await dbContext.SaveChangesAsync();

	#endregion
});


app.Run();

