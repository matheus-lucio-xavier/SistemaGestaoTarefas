using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(string eventType, string json);
    }
}