using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> Consultar<T>() where T : class;

        // Retorna nullable pois o registro pode não existir
        Task<T?> ConsultarPorId<T>(Guid id) where T : class;

        Task<bool> Cadastrar<T>(T model) where T : class;
        Task<bool> Editar<T>(T model) where T : class;
        Task<bool> Excluir<T>(T model) where T : class;
    }
}