using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Roulette.Model;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
namespace Roulette.Data.Providers.Redis
{
    public static class Redis
    {
        private static Lazy<ConnectionMultiplexer> _lazyConnection;
        private static bool isInitialized;
        public static bool Initialized { get { return isInitialized; } }
        public static void Initialize(IConfiguration configuration)
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                string cacheConnection = configuration.GetConnectionString("RedisConnectionString");
                return ConnectionMultiplexer.Connect(cacheConnection);
            });
            isInitialized = true;
        }
        public static void SaveObject<T>(T objToSave) where T : IGuidable
        {
            
            var redisDatabase = _lazyConnection.Value.GetDatabase();
            redisDatabase.StringSet(objToSave.Id.ToString(), JsonConvert.SerializeObject(objToSave));
        }
        public static T GetObject<T>(Guid id)
        {
            var redisDatabase = _lazyConnection.Value.GetDatabase();
            return JsonConvert.DeserializeObject<T>(redisDatabase.StringGet(id.ToString()));
        }
        public async static Task DeleteObjectAsync(Guid id)
        {
            var redisDatabase = _lazyConnection.Value.GetDatabase();
            await redisDatabase.KeyDeleteAsync(id.ToString());
        }
    }
}
