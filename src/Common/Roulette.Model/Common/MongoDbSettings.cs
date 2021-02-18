namespace Roulette.Model
{
    public sealed class MongoDbSettings
    {
        public string DataBaseName { get; set; }
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}
