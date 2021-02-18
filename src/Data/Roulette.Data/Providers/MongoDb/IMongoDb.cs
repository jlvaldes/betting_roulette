using Roulette.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Data.Providers.MongoDb
{
    public interface IMongoDb
    {
        Task InsertItemAsync(object entity, MongoDbSettings mongoDbSettings);
        Task<List<T>> GetItemsAsync<T>(IEnumerable<MongoDbFilter> filters, MongoDbSettings mongoDbSettings);
        Task DeleteItemsAsync(IEnumerable<MongoDbFilter> filters, MongoDbSettings mongoDbSettings);
        Task UpdateItemsAsync(IEnumerable<MongoDbFilter> filters, object entity, MongoDbSettings mongoDbSettings);
    }
}
