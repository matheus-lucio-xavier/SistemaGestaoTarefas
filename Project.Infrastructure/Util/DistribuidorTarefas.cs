using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Communication.Enum;
using Project.Domain.Entities;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Util
{
    public class DistribuidorTarefas : IDistribuidorTarefas
    {
        private readonly AppDbContext _context;

        public DistribuidorTarefas(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FuncionarioModel?> SelecionarFuncionarioAsync(AreaAtuacaoEnum areaAtuacao)
        {

            return await _context.Funcionarios
                .Where(f => f.AreaAtuacao == areaAtuacao)
                .OrderBy(f => f.Tarefas.Count)
                .FirstOrDefaultAsync();
        }
    }
}