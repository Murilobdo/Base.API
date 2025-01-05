namespace Base.Core.Interfaces.Cache;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class;
    Task SetAsync<T>(string key) where T : class;
}