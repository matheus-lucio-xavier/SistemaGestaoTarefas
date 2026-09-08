using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Communication.Dto.Events;
using Project.Domain.Entities;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Messaging.Handlers
{
    public class PedidoCriadoHandler : IEventHandler<PedidoCriadoEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDistribuidorTarefas _distribuidor;
        private readonly IUnitOfWork _unit;

        public PedidoCriadoHandler(AppDbContext dbContext, IDistribuidorTarefas distribuidor, IUnitOfWork unit)
        {
            _appDbContext = dbContext;
            _distribuidor = distribuidor;
            _unit = unit;
        }

        public async Task HandleAsync(PedidoCriadoEvent @event)
        {
           await _unit.BeginTransaction();

            try
            {
                var pedido = await _appDbContext.Pedidos.FindAsync(@event.PedidoId);

                if (pedido == null)
                    throw new Exception($"Pedido {@event.PedidoId} não encontrado.");

                var funcionario = await _distribuidor.SelecionarFuncionarioAsync(pedido.AreaAtuacao);

                if (funcionario == null)
                    throw new Exception("Não existem funcionários disponíveis.");

                var nova = new TarefaModel
                {
                    Descricao = pedido.Descricao,
                    PedidoId = pedido.Id,
                    FuncionarioId = funcionario.Id
                };

                await _appDbContext.Tarefas.AddAsync(nova);
                
                await _unit.Commit();
                await _unit.CommitTransaction();
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                Console.WriteLine("Erros: {}", ex.Data);
            }

        }
    }
}