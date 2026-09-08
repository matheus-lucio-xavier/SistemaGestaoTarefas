using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Communication.Enum;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces
{
    public interface IDistribuidorTarefas
    {
        Task<FuncionarioModel?> SelecionarFuncionarioAsync(AreaAtuacaoEnum areaAtuacao);
    }
}