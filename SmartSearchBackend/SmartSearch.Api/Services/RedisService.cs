using StackExchange.Redis;

namespace SmartSearch.Api.Services
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;
        public RedisService(IConfiguration configuration) 
        {
            _redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
        }
        public async Task<Dictionary<string, string>> GetDict(string key)
        {
            Dictionary<string, string> result = [];
            IDatabase database = _redis.GetDatabase();
            var hashTable = await database.HashGetAllAsync(key);
            foreach (var item in hashTable)
            {
                result.Add(item.Name, item.Value);
            }
            return result;
        }
        public async Task SetDict(string key, Dictionary<string, string> dict)
        {
            IDatabase database = _redis.GetDatabase();
            HashEntry[] hashEntries = new HashEntry[dict.Count];
            int i = 0;
            foreach (var item in dict)
            {
                var entry = new HashEntry(item.Key, item.Value);
                hashEntries[i] = entry;
                i++;
            }
            await database.HashSetAsync(key, hashEntries);
        }
    }
}
