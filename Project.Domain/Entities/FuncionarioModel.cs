using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class FuncionarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<TarefaModel> Tarefas { get; set; } = new List<TarefaModel>();
        public bool Disponivel { get; set; }
    }
}