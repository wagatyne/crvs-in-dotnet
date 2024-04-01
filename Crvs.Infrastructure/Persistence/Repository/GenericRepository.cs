using Crvs.Core.Contracts;
using Crvs.Domain;
using Crvs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CrvsDbContext _crvsDbContext;
        public GenericRepository(CrvsDbContext crvsDbContext)
        {
            _crvsDbContext = crvsDbContext;
        }
        public async Task AddAsync(T entity, CancellationToken token = default)
        {
            await Task.CompletedTask;
            _crvsDbContext.Add(entity);
            await _crvsDbContext.SaveChangesAsync(token);
        }

        public async Task<T?> GetByIdAsync(object id, CancellationToken token = default)
        {
            return await _crvsDbContext.Set<T>().FindAsync(new []{ id }, cancellationToken: token);
        }

        public async Task<IEnumerable<T>> SearchAsync(SearchParams<T> searchParams, CancellationToken token = default)
        {
            IQueryable<T> dbSet =  _crvsDbContext.Set<T>().AsNoTracking();

            if(searchParams.Query is not null) dbSet = dbSet.Where(searchParams.Query);
            if(searchParams.ItemsToReturn > 0) dbSet = dbSet.Take(searchParams.ItemsToReturn);
            if(searchParams.ItemsToSkip > 0) dbSet.Skip(searchParams.ItemsToSkip);

            return await dbSet.ToListAsync(token);
        }

        public async Task UpdateAsync(T entity, CancellationToken token = default)
        {
            _crvsDbContext.Set<T>().Update(entity);
            await _crvsDbContext.SaveChangesAsync(token);
        }
    }

}
