using Roulette.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class BetRedisRepository : AbstractBetRedisRepository
    {
        public override Task<Bet> CreateAsync(Bet entity)
        {
            throw new NotImplementedException();
        }
        public override Task<Bet> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public override Task<Bet> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public override Task<Bet> UpdateAsync(Bet entity)
        {
            throw new NotImplementedException();
        }
    }
}
