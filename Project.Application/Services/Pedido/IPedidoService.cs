using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Communication.Dto.Requests;
using Project.Communication.Dto.Responses;
using Project.Domain.Entities;

namespace Project.Application.Services
{
    public interface IPedidoService
    {
        Task<ServiceResponse<List<PedidoModel>>> Consultar();
        Task<ServiceResponse<PedidoModel>> ConsultarPorId(Guid id);
        Task<ServiceResponse<PedidoModel>> Cadastrar(RequestPedidoRegisterJson pedido);
        Task<ServiceResponse<PedidoModel>> Deletar(Guid id);
    }
}