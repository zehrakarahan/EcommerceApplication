using EcommerceApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Application.Services
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        ///     Iqueryable entity of Entity Framework. Use this to execute query in database level.
        /// </summary>
        IQueryable<T> Entity { get; }

 
        Task<T> GetByIdAsync(int id);

        Task<int> CountTotalAsync();

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetListAsync(params Expression<Func<T, object>>[] includes);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task UpdateAsyncEmpty(T entity);

        Task<bool> UpdateAsyncBool(int Id,T entity);

        Task DeleteAsync(T entity);

        Task<bool> DeleteAsync(int Id);
    }

}
