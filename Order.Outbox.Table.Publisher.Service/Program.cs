using MassTransit;
using Order.Outbox.Table.Publisher.Service;
using Order.Outbox.Table.Publisher.Service.Jobs;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMassTransit(x =>
{
	x.UsingRabbitMq((_context, _configurator) =>
	{
		_configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
	});
});
builder.Services.AddQuartz(configurator =>
{
	var jobKey = new JobKey("OrderOutboxPublishJob");
	configurator.AddJob<OrderOutboxPublishJob>(options => options.WithIdentity(jobKey));

	var triggerKey = new TriggerKey("OrderOutboxPublishTrigger");
	configurator.AddTrigger(options => options.ForJob(jobKey)
	.WithIdentity(triggerKey)
	.StartAt(DateTime.UtcNow)
	.WithSimpleSchedule(builder => builder.WithIntervalInSeconds(5).RepeatForever())
	);
});

builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

var host = builder.Build();
host.Run();
