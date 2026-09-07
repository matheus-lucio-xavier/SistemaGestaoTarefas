using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Project.Communication.Dto.Events;
using Project.Domain.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Project.Worker;

public class Worker : BackgroundService
{
    private readonly RabbitMQSettings _settings;

    public Worker(IOptions<RabbitMQSettings> settings)
    {
        _settings = settings.Value;
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
            var body = args.Body.ToArray();

            var json = Encoding.UTF8.GetString(body);

            var evento = JsonSerializer.Deserialize<PedidoCriadoEvent>(json);

            if (evento == null)
            {
                Console.WriteLine("Não foi possível desserializar a mensagem.");
                return;
            }

            Console.WriteLine($"Pedido recebido: {evento.PedidoId}");

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
