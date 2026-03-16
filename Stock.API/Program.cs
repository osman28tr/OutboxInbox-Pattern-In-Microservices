using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Stock.API.Consumers;
using Stock.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<OrderCreatedEventConsumer>();
	x.UsingRabbitMq((_context, _configurator) =>
	{
		_configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

		_configurator.ReceiveEndpoint(RabbitMQSettings.Stock_OrderCreatedEvent, e =>
		{
			e.ConfigureConsumer<OrderCreatedEventConsumer>(_context);
		});
	});
	
});

var app = builder.Build();



app.Run();

