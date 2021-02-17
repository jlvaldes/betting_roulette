using Roulette.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class RouletteRedisRepository : AbstractRouletteRedisRepository
    {
        public override Task<IRoulette> CreateAsync(IRoulette entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IRoulette> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<IRoulette> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<IRoulette> UpdateAsync(IRoulette entity)
        {
            throw new NotImplementedException();
        }
    }
}
