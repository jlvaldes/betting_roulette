using Microsoft.Extensions.Options;
using Roulette.Data.Providers.MongoDb;
using Roulette.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class BetMongoDbRepository : AbstractBetMongoDbRepository
    {
        private readonly IMongoDb _mongoDb;
        private readonly MongoDbSettings _betMongoDbSettings;
        public BetMongoDbRepository(IMongoDb mongoDb, IOptions<RouletteSettings> rouleteSettings)
        {
            _mongoDb = mongoDb;
            _betMongoDbSettings = rouleteSettings.Value.BetMongoDbSettings;
        }
        public async override Task<Bet> CreateAsync(Bet entity)
        {
            await _mongoDb.InsertItemAsync(entity, _betMongoDbSettings);
            return entity;
        }
        public async override Task DeleteByCodeAsync(string code)
        {
            await _mongoDb.DeleteItemsAsync(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Code",
                    Value = code,
                    Operator = FilterOperator.Equal
                }
            }, _betMongoDbSettings);
        }
        public async override Task<Bet> FindByCodeAsync(string code)
        {
            return (await _mongoDb.GetItemsAsync<Bet>(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Code",
                    Value = code,
                    Operator = FilterOperator.Equal
                }
            }, _betMongoDbSettings)).FirstOrDefault();
        }
        public async override Task<IEnumerable<Bet>> FindByStringsFiltersAsync(Dictionary<string, string> filters)
        {
            return await _mongoDb.GetItemsAsync<Bet>(filters.Select(x => new MongoDbFilter
            {
                FieldName = x.Key,
                Value = x.Value,
                Operator = FilterOperator.Equal
            }).ToList(), _betMongoDbSettings);
        }
        public async override Task<Bet> UpdateAsync(Bet entity)
        {
            await _mongoDb.UpdateItemsAsync(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Code",
                    Value = entity.Code,
                    Operator = FilterOperator.Equal
                }
            }, entity, _betMongoDbSettings);
            return entity;
        }
    }
}
