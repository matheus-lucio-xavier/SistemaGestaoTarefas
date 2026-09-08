using System.Text.Json;
using Project.Communication.Dto.Events;
using Project.Domain.Interfaces;

namespace Project.Infrastructure.Messaging
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(
            string eventType,
            string json)
        {
            switch (eventType)
            {
                case "PedidoCriado":
                {
                    var @event = JsonSerializer.Deserialize<PedidoCriadoEvent>(json);

                    if (@event == null)
                        throw new Exception("Evento inválido.");

                    var handler = _serviceProvider.GetRequiredService<IEventHandler<PedidoCriadoEvent>>();

                    await handler.HandleAsync(@event);

                    break;
                }

                default:
                    throw new Exception(
                        $"Evento desconhecido: {eventType}");
            }
        }
    }
}