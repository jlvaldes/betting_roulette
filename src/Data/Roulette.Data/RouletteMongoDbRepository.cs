using Microsoft.Extensions.Options;
using Roulette.Data.Providers.MongoDb;
using Roulette.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public sealed class RouletteMongoDbRepository : AbstractRouletteMongoDbRepository
    {
        private readonly IMongoDb _mongoDb;
        private readonly MongoDbSettings _rouletteMongoDbSettings;
        public RouletteMongoDbRepository(IMongoDb mongoDb, IOptions<RouletteSettings> rouleteSettings)
        {
            _mongoDb = mongoDb;
            _rouletteMongoDbSettings = rouleteSettings.Value.RouletteMongoDbSettings;
        }
        public override async Task<Model.Roulette> CreateAsync(Model.Roulette entity)
        {
            await _mongoDb.InsertItemAsync(entity, _rouletteMongoDbSettings);
            return entity;
        }
        public async override Task DeleteByCodeAsync(string code)
        {
            await _mongoDb.DeleteItemsAsync(new List<MongoDbFilter>
            {
                new MongoDbFilter 
                {
                    FieldName = "Id",
                    Value = code,
                    Operator = FilterOperator.Equal
                } 
            }, _rouletteMongoDbSettings);
        }
        public async override Task<Model.Roulette> FindByCodeAsync(string code)
        {
            return (await _mongoDb.GetItemsAsync<Model.Roulette>(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Code",
                    Value = code,
                    Operator = FilterOperator.Equal
                }
            }, _rouletteMongoDbSettings)).FirstOrDefault();
        }
        public async override Task<Model.Roulette> UpdateAsync(Model.Roulette entity)
        {
            await _mongoDb.DeleteItemsAsync(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Code",
                    Value = entity.Code,
                    Operator = FilterOperator.Equal
                }
            }, _rouletteMongoDbSettings);
            await _mongoDb.InsertItemAsync(entity, _rouletteMongoDbSettings);
            return entity;
        }
        public override async Task<IEnumerable<Model.Roulette>> FindByStringsFiltersAsync(Dictionary<string, string> filters)
        {
            return await _mongoDb.GetItemsAsync<Model.Roulette>(filters?.Select(x => new MongoDbFilter
            {
                FieldName = x.Key,
                Value = x.Value,
                Operator = FilterOperator.Equal
            }).ToList(), _rouletteMongoDbSettings);
        }
    }
}
