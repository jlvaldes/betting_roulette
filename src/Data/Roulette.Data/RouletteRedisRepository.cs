using Roulette.Data.Providers.Redis;
using Roulette.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class RouletteRedisRepository : AbstractRouletteRedisRepository
    {
        public override Task<IRoulette> CreateAsync(IRoulette entity)
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
        public override Task<IRoulette> FindByCodeAsync(string code)
        {
            return Task.Run(() =>
            {
                return Redis.GetObject<IRoulette>(code);
            });

        }
        public override Task<IRoulette> UpdateAsync(IRoulette entity)
        {
            return Task.Run(() =>
            {
                Redis.SaveObject(entity);
                return entity;
            });
        }
        public override Task<IEnumerable<IRoulette>> FindByStringsFiltersAsync(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }
    }
}
