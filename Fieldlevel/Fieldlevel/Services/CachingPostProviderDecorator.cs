using Fieldlevel.Models;
using Fieldlevel.Settings;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Fieldlevel.Services
{
    public class CachingPostProviderDecorator : IPostProvider
    {
        private readonly IPostProvider _postProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;

        private const string CacheKey = "All_Posts";

        public CachingPostProviderDecorator(
            IPostProvider postProvider, 
            IMemoryCache memoryCache,
            CacheSettings cacheSettings)
        {
            _postProvider = postProvider;
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings;
        }

        public IEnumerable<Post> GetPosts()
            => _memoryCache.GetValue(CacheKey, _cacheSettings.PostsCacheMinutes, () => _postProvider.GetPosts());
    }
}
