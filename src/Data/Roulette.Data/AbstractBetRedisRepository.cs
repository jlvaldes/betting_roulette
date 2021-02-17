using Roulette.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractBetRedisRepository : IRepository<Bet>
    {
        public StorageProvider StorageProvider => StorageProvider.MongoDb;
        public abstract Task<Bet> CreateAsync(Bet entity);
        public abstract Task<Bet> DeleteByIdAsync(Guid id);
        public abstract Task<Bet> FindByIdAsync(Guid id);
        public abstract Task<Bet> UpdateAsync(Bet entity);
    }
}
