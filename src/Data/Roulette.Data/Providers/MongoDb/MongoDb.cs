using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Roulette.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Roulette.Data.Providers.MongoDb
{
    public class MongoDb : IMongoDb
    {
        public async Task InsertItemAsync(object entity, MongoDbSettings mongoDbSettings)
        {
            await GetCollectionDatabase(mongoDbSettings).InsertOneAsync(entity.ToBsonDocument());
        }
        public async Task<List<T>> GetItemsAsync<T>(IEnumerable<MongoDbFilter> filters, MongoDbSettings mongoDbSettings)
        {
            var data = filters != null && filters.Any()
                ? await GetCollectionDatabase(mongoDbSettings).Find(SerializeFilter(filters)).Project(Builders<BsonDocument>.Projection.Exclude("_id").Exclude("_t")).ToListAsync()
                : await GetCollectionDatabase(mongoDbSettings).Find(Builders<BsonDocument>.Filter.Empty).Project(Builders<BsonDocument>.Projection.Exclude("_id").Exclude("_t")).ToListAsync();
            List<T> returnList = new List<T>();
            foreach (var l in data)
            {
                returnList.Add(BsonSerializer.Deserialize<T>(l));
            }
            return returnList;
        }
        public async Task DeleteItemsAsync(IEnumerable<MongoDbFilter> filters, MongoDbSettings mongoDbSettings)
        {
            await GetCollectionDatabase(mongoDbSettings).DeleteManyAsync(SerializeFilter(filters));
        }
        public async Task UpdateItemsAsync(IEnumerable<MongoDbFilter> filters, object entity, MongoDbSettings mongoDbSettings)
        {
            FilterDefinition<BsonDocument> filterDefinition = SerializeFilter(filters);
            UpdateDefinition<BsonDocument> updateDefinition = entity.ToBsonDocument();
            await GetCollectionDatabase(mongoDbSettings).UpdateManyAsync(filterDefinition, updateDefinition);
        }
        private IMongoCollection<BsonDocument> GetCollectionDatabase(MongoDbSettings mongoDbSettings)
        {
            return new MongoClient(mongoDbSettings.ConnectionString).GetDatabase(mongoDbSettings.DataBaseName).GetCollection<BsonDocument>(mongoDbSettings.CollectionName);
        }
        private FilterDefinition<BsonDocument> SerializeFilter(IEnumerable<MongoDbFilter> filters)
        {
            FilterDefinition<BsonDocument> filterDefinition = null;
            foreach (MongoDbFilter filter in filters)
            {
                if (filterDefinition is null)
                {
                    filterDefinition = GetFilter(filter);
                }
                else
                {
                    filterDefinition = filterDefinition & GetFilter(filter);
                }
            }
            return filterDefinition;
        }
        private FilterDefinition<BsonDocument> GetFilter(MongoDbFilter filter)
        {
            FilterDefinitionBuilder<BsonDocument> filterBuilder = Builders<BsonDocument>.Filter;
            var expressions = new Dictionary<FilterOperator, Func<FilterDefinitionBuilder<BsonDocument>, MongoDbFilter, FilterDefinition<BsonDocument>>>
            {
                {FilterOperator.Equal, (builder, filter) => builder.Eq(filter.FieldName, BsonValue.Create(filter.Value))},
                { FilterOperator.GreaterThan, (builder, filter) => builder.Gt(filter.FieldName, BsonValue.Create(filter.Value))},
                {FilterOperator.GreaterThanOrEqual, (builder, filter) => builder.Gte(filter.FieldName, BsonValue.Create(filter.Value)) },
                { FilterOperator.LessThan, (builder, filter) => builder.Lt(filter.FieldName, BsonValue.Create(filter.Value))},
                {FilterOperator.LessThanOrEqual, (builder, filter) => builder.Lte(filter.FieldName, BsonValue.Create(filter.Value)) },
                {FilterOperator.NotEqual, (builder, filter) => builder.Ne(filter.FieldName, BsonValue.Create(filter.Value)) }
            };
            return !expressions.TryGetValue(filter.Operator, out Func<FilterDefinitionBuilder<BsonDocument>, MongoDbFilter, FilterDefinition<BsonDocument>> filterDefinition) ? null : filterDefinition(filterBuilder, filter);
        }
    }
}
