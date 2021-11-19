using Microsoft.Extensions.Caching.Memory;
using System;

namespace Fieldlevel
{
    public static class CacheExtensions
    {
        public static T GetValue<T>(this IMemoryCache cache, string cacheKey, int minutesToCache, Func<T> valueFetcher)
        {
            if (!cache.TryGetValue(cacheKey, out var value))
            {
                value = valueFetcher();
                cache.Set(cacheKey, value, new TimeSpan(0, minutesToCache, 0));
            }
            return (T)value;
        }
    }
}
