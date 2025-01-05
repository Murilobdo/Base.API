using Base.Core.Interfaces.Cache;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Base.Infra.Cache;

public class CacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;

    public CacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }


    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiration);
    }

    public async Task SetAsync<T>(string key) where T : class
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(key, JsonConvert.SerializeObject(default(T)));
    }
}
