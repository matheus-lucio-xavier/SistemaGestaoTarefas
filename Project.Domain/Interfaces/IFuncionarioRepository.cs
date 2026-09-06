using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces
{
    public interface IFuncionarioRepository : IRepository
    {
        IQueryable<TarefaModel> ConsultarTarefasPorFuncionario(Guid id);
    }
}