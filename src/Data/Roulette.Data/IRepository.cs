using Roulette.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public interface IRepository<T> where T : class
    {
        StorageProvider StorageProvider { get; }
        Task<T> FindByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> FindByStringsFiltersAsync(Dictionary<string, string> filters);
    }
}
