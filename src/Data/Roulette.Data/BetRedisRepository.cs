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
        public async override Task DeleteByCodeAsync(string code)
        {
            await Redis.DeleteObjectAsync(code);
        }
        public override Task<IBet> FindByCodeAsync(string code)
        {
            return Task.Run(() =>
            {
                return Redis.GetObject<IBet>(code);
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
