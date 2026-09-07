using Project.Domain.Settings;
using Project.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
