using Closify.Data;
using Closify.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Closify.Repository.Impl
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ClosifyContext _context;

        public GenericRepository(ClosifyContext context)
        {
            _context = context;
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public abstract Task<bool> Exists(int id);

        /*
        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }
        */

        public Task<List<TEntity>> GetAll()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
