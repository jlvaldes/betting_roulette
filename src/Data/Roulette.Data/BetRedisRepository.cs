using Roulette.Data.Providers.Redis;
using Roulette.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class BetRedisRepository : AbstractBetRedisRepository
    {
        public override Task<IBet> CreateAsync(IBet entity)
        {
            return Task.Run(() =>
            {
                Redis.SaveObject(entity);
                return entity;
            });
        }
        public async override Task DeleteByIdAsync(Guid id)
        {
            await Redis.DeleteObjectAsync(id);
        }
        public override Task<IBet> FindByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                return Redis.GetObject<IBet>(id);
            });
            
        }
        public override Task<IBet> UpdateAsync(IBet entity)
        {
            return Task.Run(() =>
            {
                Redis.SaveObject(entity);
                return entity;
            });
        }
        public override Task<IEnumerable<IBet>> FindByStringsFiltersAsync(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }
    }
}
