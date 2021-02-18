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
        public override async Task<IRoulette> CreateAsync(IRoulette entity)
        {
            await _mongoDb.InsertItemAsync(entity, _rouletteMongoDbSettings);
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
            }, _rouletteMongoDbSettings);
        }
        public async override Task<IRoulette> FindByIdAsync(Guid id)
        {
            return (await _mongoDb.GetItemsAsync<IRoulette>(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Id",
                    Value = id.ToString(),
                    Operator = FilterOperator.Equal
                }
            }, _rouletteMongoDbSettings)).FirstOrDefault();
        }
        public async override Task<IRoulette> UpdateAsync(IRoulette entity)
        {
            await _mongoDb.UpdateItemsAsync(new List<MongoDbFilter>
            {
                new MongoDbFilter
                {
                    FieldName = "Id",
                    Value = entity.Id.ToString(),
                    Operator = FilterOperator.Equal
                }
            }, entity, _rouletteMongoDbSettings);
            return entity;
        }
        public override Task<IEnumerable<IRoulette>> FindByStringsFiltersAsync(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }
    }
}
