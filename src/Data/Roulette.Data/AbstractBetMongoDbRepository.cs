using Roulette.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractBetMongoDbRepository : IRepository<Bet>
    {
        public StorageProvider StorageProvider => StorageProvider.MongoDb;
        public abstract Task<Bet> CreateAsync(Bet entity);
        public abstract Task DeleteByCodeAsync(string code);
        public abstract Task<Bet> FindByCodeAsync(string code);
        public abstract Task<IEnumerable<Bet>> FindByStringsFiltersAsync(Dictionary<string, string> filters);
        public abstract Task<Bet> UpdateAsync(Bet entity);
    }
}