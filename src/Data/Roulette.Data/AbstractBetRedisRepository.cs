using Roulette.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractBetRedisRepository : IRepository<IBet>
    {
        public StorageProvider StorageProvider => StorageProvider.Redis;
        public abstract Task<IBet> CreateAsync(IBet entity);
        public abstract Task DeleteByCodeAsync(string code);
        public abstract Task<IBet> FindByCodeAsync(string code);
        public abstract Task<IEnumerable<IBet>> FindByStringsFiltersAsync(Dictionary<string, string> filters);
        public abstract Task<IBet> UpdateAsync(IBet entity);
    }
}
