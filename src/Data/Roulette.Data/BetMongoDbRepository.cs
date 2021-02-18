using Microsoft.Extensions.Options;
using Roulette.Data.Providers.MongoDb;
using Roulette.Model;
using System;
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
        public async override Task<IBet> CreateAsync(IBet entity)
        {
            await _mongoDb.InsertItemAsync(entity, _betMongoDbSettings);
            return entity;
        }
        public async override Task DeleteByIdAsync(Guid id)
        {
            await _mongoDb.DeleteItemsAsync(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Id",
                    Value = id.ToString(),
                    Operator = FilterOperator.Equal
                }
            }, _betMongoDbSettings);
        }
        public async override Task<IBet> FindByIdAsync(Guid id)
        {
            return (await _mongoDb.GetItemsAsync<IBet>(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Id",
                    Value = id.ToString(),
                    Operator = FilterOperator.Equal
                }
            }, _betMongoDbSettings)).FirstOrDefault();
        }
        public async override Task<IEnumerable<IBet>> FindByStringsFiltersAsync(Dictionary<string, string> filters)
        {
            return await _mongoDb.GetItemsAsync<IBet>(filters.Select(x => new MongoDbFilter
            {
                FieldName = x.Key,
                Value = x.Value,
                Operator = FilterOperator.Equal
            }).ToList(), _betMongoDbSettings);
        }
        public async override Task<IBet> UpdateAsync(IBet entity)
        {
            await _mongoDb.UpdateItemsAsync(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Id",
                    Value = entity.Id.ToString(),
                    Operator = FilterOperator.Equal
                }
            }, entity, _betMongoDbSettings);
            return entity;
        }
    }
}
