using Roulette.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractRouletteMongoDbRepository : IRepository<Model.Roulette>
    {
        public StorageProvider StorageProvider => StorageProvider.MongoDb;
        public abstract Task<Model.Roulette> CreateAsync(Model.Roulette entity);
        public abstract Task DeleteByCodeAsync(string code);
        public abstract Task<Model.Roulette> FindByCodeAsync(string code);
        public abstract Task<IEnumerable<Model.Roulette>> FindByStringsFiltersAsync(Dictionary<string, string> filters);
        public abstract Task<Model.Roulette> UpdateAsync(Model.Roulette entity);
    }
}
