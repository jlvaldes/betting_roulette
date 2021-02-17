using Roulette.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class BetRedisRepository : AbstractBetRedisRepository
    {
        public override Task<IBet> CreateAsync(IBet entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IBet> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<IBet> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<IBet> UpdateAsync(IBet entity)
        {
            throw new NotImplementedException();
        }
    }
}
