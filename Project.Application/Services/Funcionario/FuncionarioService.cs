using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Communication.Dto.Requests;
using Project.Communication.Dto.Responses;
using Project.Domain.Entities;
using Project.Domain.Interfaces;

namespace Project.Application.Services.Funcionario
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _repository;
        private readonly IUnitOfWork _unit;

        public FuncionarioService(IFuncionarioRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unit = unitOfWork;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> Consultar()
        {
            try
            {
                var query = _repository.Consultar<FuncionarioModel>().OrderBy(p => p.Id);

                var pedidos = await query.ToListAsync();

                return ServiceResponse<List<FuncionarioModel>>.Ok(pedidos);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<FuncionarioModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<TarefaModel>>> ConsultarTarefas(Guid id)
        {
            try
            {
                var funcionario = _repository.ConsultarPorId<FuncionarioModel>(id);

                if (funcionario == null)
                    return ServiceResponse<List<TarefaModel>>.BadRequest("Funcionario nao existe");

                var tarefas = await _repository.ConsultarTarefasPorFuncionario(id).ToListAsync();

                return ServiceResponse<List<TarefaModel>>.Ok(tarefas);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<TarefaModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<FuncionarioModel>> ConsultarPorId(Guid id)
        {
            try
            {
                var result =  await _repository.ConsultarPorId<FuncionarioModel>(id);

                if (result == null)
                {
                    return ServiceResponse<FuncionarioModel>.BadRequest("Pedido nao existe");
                }

                return ServiceResponse<FuncionarioModel>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<FuncionarioModel>.Error(ex.Message);
            }
        }
        public async Task<ServiceResponse<FuncionarioModel>> Cadastrar(RequestFuncionarioRegisterJson funcionario)
        {
            await _unit.BeginTransaction();

            try
            {
                var novo = new FuncionarioModel
                {
                    Nome = funcionario.Nome,
                    AreaAtuacao = funcionario.AreaAtuacao
                };

                await _repository.Cadastrar(novo);
                
                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<FuncionarioModel>.Ok(novo);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<FuncionarioModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<FuncionarioModel>> Deletar(Guid id)
        {

            try
            {
                var existente = await _repository.ConsultarPorId<FuncionarioModel>(id);

                if (existente == null)
                {
                    return ServiceResponse<FuncionarioModel>.BadRequest("Funcionario nao existe");
                }

                _repository.Excluir(existente);
                var saved = await _unit.Commit();

                if (saved)
                {
                    return ServiceResponse<FuncionarioModel>.Ok(existente);
                }

                return ServiceResponse<FuncionarioModel>.Error("Nao foi possivel deletar esse pedido");
            }
            catch (Exception ex)
            {
                return ServiceResponse<FuncionarioModel>.Error(ex.Message);
            }
        }
    }
}