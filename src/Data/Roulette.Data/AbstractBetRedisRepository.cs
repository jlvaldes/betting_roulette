using Roulette.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractBetRedisRepository : IRepository<IBet>
    {
        public StorageProvider StorageProvider => StorageProvider.Redis;
        public abstract Task<IBet> CreateAsync(IBet entity);
        public abstract Task DeleteByIdAsync(Guid id);
        public abstract Task<IBet> FindByIdAsync(Guid id);
        public abstract Task<IEnumerable<IBet>> FindByStringsFiltersAsync(Dictionary<string, string> filters);
        public abstract Task<IBet> UpdateAsync(IBet entity);
    }
}
