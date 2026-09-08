using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Project.Communication.Dto.Events;
using Project.Domain.Interfaces;
using Project.Domain.Settings;
using Project.Infrastructure.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Project.Worker;

public class Worker : BackgroundService
{
    private readonly RabbitMQSettings _settings;
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(IOptions<RabbitMQSettings> settings, IServiceScopeFactory scopeFactory)
    {
        _settings = settings.Value;
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port
        };

        var connection = await factory.CreateConnectionAsync();

        var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: "sistema.tarefas",
            type: ExchangeType.Topic,
            durable: true);

        await channel.QueueDeclareAsync(
            queue: "pedido.criado",
            durable: true,
            exclusive: false,
            autoDelete: false);

        await channel.QueueBindAsync(
            queue: "pedido.criado",
            exchange: "sistema.tarefas",
            routingKey: "pedido.criado");

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, args) =>
        {
            var json = Encoding.UTF8.GetString(args.Body.ToArray());

            var message = JsonSerializer.Deserialize<EventMessage>(json);

            if (message == null)
            {
                throw new Exception("Mensagem inválida.");
            }

            using var scope = _scopeFactory.CreateScope();

            var dispatcher =
                scope.ServiceProvider
                    .GetRequiredService<IEventDispatcher>();

            await dispatcher.DispatchAsync(
                message.EventType, 
                message.Data);

            await channel.BasicAckAsync(
                deliveryTag: args.DeliveryTag,
                multiple: false);
        };

        await channel.BasicConsumeAsync(
            queue: "pedido.criado",
            autoAck: false,
            consumer: consumer);

        Console.WriteLine("Worker aguardando mensagens...");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
