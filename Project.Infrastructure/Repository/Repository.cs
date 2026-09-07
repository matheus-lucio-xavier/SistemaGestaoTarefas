using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        protected readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<T> Consultar<T>() where T : class
        {
            return _appDbContext.Set<T>().AsQueryable(); 
        }

        public async Task<T?> ConsultarPorId<T>(Guid id) where T : class
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> Cadastrar<T>(T model) where T : class
        {
            await _appDbContext.Set<T>().AddAsync(model);
            return true;
        }

        public bool Editar<T>(T model) where T : class
        {
            _appDbContext.Set<T>().Update(model);
            return true;
        }

        public bool Excluir<T>(T model) where T : class
        {
            _appDbContext.Set<T>().Remove(model);
            return true;
        }
    }
}