using System.Collections.Generic;
namespace Roulette.Model
{
    public sealed class RouletteSettings
    {
        public int MinNumberBet { get; set; }
        public int MaxNumberBet { get; set; }
        public List<string> Cors { get; set; }
        public ScaleUpStrategy ScaleUpStrategy { get; set; }
        public StorageProvider StorageProvider { get; set; }
        public MongoDbSettings RouletteMongoDbSettings { get; set; }
        public MongoDbSettings BetMongoDbSettings { get; set; }
        public RedisSettings RedisSettings { get; set; }
    }
}
