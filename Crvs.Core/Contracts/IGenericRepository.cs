using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Crvs.Domain;
using Crvs.Domain.Entities;

namespace Crvs.Core.Contracts
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T?> GetByIdAsync(object id, CancellationToken token = default);
        Task AddAsync(T entity, CancellationToken token = default);
        Task UpdateAsync(T entity, CancellationToken token = default);
        Task<IEnumerable<T>> SearchAsync(SearchParams<T> searchParams, CancellationToken token = default);
    }
}
