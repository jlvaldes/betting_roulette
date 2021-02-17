using Roulette.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractRouletteRedisRepository : IRepository<IRoulette>
    {
        public StorageProvider StorageProvider => StorageProvider.Redis;
        public abstract Task<IRoulette> CreateAsync(IRoulette entity);
        public abstract Task<IRoulette> DeleteByIdAsync(Guid id);
        public abstract Task<IRoulette> FindByIdAsync(Guid id);
        public abstract Task<IRoulette> UpdateAsync(IRoulette entity);
    }
}
