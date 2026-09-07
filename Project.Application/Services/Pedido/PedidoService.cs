using Microsoft.EntityFrameworkCore;
using Project.Communication.Dto.Requests;
using Project.Communication.Dto.Responses;
using Project.Domain.Entities;
using Project.Domain.Interfaces;

namespace Project.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unit;

        public PedidoService(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unit = unitOfWork;
        }

        public async Task<ServiceResponse<List<PedidoModel>>> Consultar()
        {
            try
            {
                var query = _repository.Consultar<PedidoModel>().OrderBy(p => p.Id);

                var pedidos = await query.ToListAsync();

                return ServiceResponse<List<PedidoModel>>.Ok(pedidos);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<PedidoModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<PedidoModel>> ConsultarPorId(Guid id)
        {
            try
            {
                var result =  await _repository.ConsultarPorId<PedidoModel>(id);

                if (result == null)
                {
                    return ServiceResponse<PedidoModel>.BadRequest("Pedido nao existe");
                }

                return ServiceResponse<PedidoModel>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<PedidoModel>.Error(ex.Message);
            }
        }
        public async Task<ServiceResponse<PedidoModel>> Cadastrar(RequestPedidoRegisterJson pedido)
        {
            await _unit.BeginTransaction();

            try
            {
                var novo = new PedidoModel
                {
                    Descricao = pedido.Descricao,
                    AreaAtuacao = pedido.AreaAtuacao
                };

                await _repository.Cadastrar(novo);
                
                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<PedidoModel>.Ok(novo);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<PedidoModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<PedidoModel>> Deletar(Guid id)
        {

            try
            {
                var existente = await _repository.ConsultarPorId<PedidoModel>(id);

                if (existente == null)
                {
                    return ServiceResponse<PedidoModel>.BadRequest("Pedido nao existe");
                }

                await _repository.Excluir(existente);
                var saved = await _unit.Commit();

                if (saved)
                {
                    return ServiceResponse<PedidoModel>.Ok(existente);
                }

                return ServiceResponse<PedidoModel>.Error("Nao foi possivel deletar esse pedido");
            }
            catch (Exception ex)
            {
                return ServiceResponse<PedidoModel>.Error(ex.Message);
            }
        }
    }
}