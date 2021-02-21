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
        public static void SaveObject<T>(T objToSave) where T : ICodificable
        {
            
            var redisDatabase = _lazyConnection.Value.GetDatabase();
            redisDatabase.StringSet(objToSave.Code, JsonConvert.SerializeObject(objToSave));
        }
        public static T GetObject<T>(string code)
        {
            var redisDatabase = _lazyConnection.Value.GetDatabase();
            return JsonConvert.DeserializeObject<T>(redisDatabase.StringGet(code));
        }
        public async static Task DeleteObjectAsync(string code)
        {
            var redisDatabase = _lazyConnection.Value.GetDatabase();
            await redisDatabase.KeyDeleteAsync(code);
        }
    }
}
