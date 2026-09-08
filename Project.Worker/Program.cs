using Microsoft.EntityFrameworkCore;
using Project.Communication.Dto.Events;
using Project.Domain.Interfaces;
using Project.Domain.Settings;
using Project.Infrastructure.Data;
using Project.Infrastructure.Messaging;
using Project.Infrastructure.Messaging.Handlers;
using Project.Infrastructure.UnitOfWork;
using Project.Infrastructure.Util;
using Project.Worker;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IEventHandler<PedidoCriadoEvent>, PedidoCriadoHandler>();

builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();

builder.Services.AddScoped<IDistribuidorTarefas, DistribuidorTarefas>();

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
