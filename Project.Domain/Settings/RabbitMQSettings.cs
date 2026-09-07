using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Settings
{
    public class RabbitMQSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}