using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Communication.Dto.Events
{
    public class EventMessage
    {
        public string EventType { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
    }
}