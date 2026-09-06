using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repository
{
    public class FuncionarioRepository : Repository, IFuncionarioRepository
    {
        public FuncionarioRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            
        }

        public IQueryable<TarefaModel> ConsultarTarefasPorFuncionario(Guid id)
        {
            return _appDbContext.Tarefas
                .Where(t => t.FuncionarioId == id)
                .Include(t => t.Funcionario)
                .OrderByDescending(t => t.Id);
        }
    }
}