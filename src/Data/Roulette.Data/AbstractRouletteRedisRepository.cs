using Roulette.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractRouletteRedisRepository : IRepository<IRoulette>
    {
        public StorageProvider StorageProvider => StorageProvider.Redis;
        public abstract Task<IRoulette> CreateAsync(IRoulette entity);
        public abstract Task DeleteByCodeAsync(string code);
        public abstract Task<IRoulette> FindByCodeAsync(string code);
        public abstract Task<IEnumerable<IRoulette>> FindByStringsFiltersAsync(Dictionary<string, string> filters);
        public abstract Task<IRoulette> UpdateAsync(IRoulette entity);
    }
}
