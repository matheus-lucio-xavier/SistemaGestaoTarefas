using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Communication.Enum;

namespace Project.Domain.Entities
{
    public class FuncionarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public AreaAtuacaoEnum AreaAtuacao { get; set; }
        public ICollection<TarefaModel> Tarefas { get; set; } = new List<TarefaModel>();
    }
}