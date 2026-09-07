using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Project.Domain.Interfaces;
using Project.Domain.Settings;
using RabbitMQ.Client;

namespace Project.Application.Services
{
    public class EventPublisher : IEventPublisher
    {

        private readonly RabbitMQSettings _settings;

        public EventPublisher(IOptions<RabbitMQSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task PublishAsync<T>(T @event)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.Host,
                Port = _settings.Port
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: "sistema.tarefas",
                type: ExchangeType.Topic,
                durable: true);

            var json = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "sistema.tarefas",
                routingKey: "pedido.criado",
                body: body);
        }
    }
}