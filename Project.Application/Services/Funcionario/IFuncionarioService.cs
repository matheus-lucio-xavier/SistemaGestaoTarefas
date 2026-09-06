using Project.Communication.Dto.Requests;
using Project.Communication.Dto.Responses;
using Project.Domain.Entities;

namespace Project.Application.Services.Funcionario
{
    public interface IFuncionarioService
    {
        Task<ServiceResponse<List<FuncionarioModel>>> Consultar();
        Task<ServiceResponse<List<TarefaModel>>> ConsultarTarefas(Guid id);
        Task<ServiceResponse<FuncionarioModel>> ConsultarPorId(Guid id);
        Task<ServiceResponse<FuncionarioModel>> Cadastrar(RequestFuncionarioRegisterJson pedido);
        Task<ServiceResponse<FuncionarioModel>> Deletar(Guid id);
    }
}