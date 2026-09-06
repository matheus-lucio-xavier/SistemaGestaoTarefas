using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Communication.Enum;

namespace Project.Domain.Entities
{
    public class TarefaModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Guid FuncionarioId { get; set; }
        public FuncionarioModel Funcionario { get; set; } = null!;
        public Guid PedidoId { get; set; }
        public PedidoModel Pedido { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}