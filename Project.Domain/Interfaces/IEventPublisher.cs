using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event);
    }
}