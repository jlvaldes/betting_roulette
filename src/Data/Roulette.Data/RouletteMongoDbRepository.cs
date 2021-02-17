using System;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class RouletteMongoDbRepository : AbstractRouletteMongoDbRepository
    {
        public override Task<Model.Roulette> CreateAsync(Model.Roulette entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Model.Roulette> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<Model.Roulette> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<Model.Roulette> UpdateAsync(Model.Roulette entity)
        {
            throw new NotImplementedException();
        }
    }
}
