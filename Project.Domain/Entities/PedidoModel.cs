using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Communication.Enum;

namespace Project.Domain.Entities
{
    public class PedidoModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public PedidoStatus Status { get; set; }
        public AreaAtuacaoEnum AreaAtuacao { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}