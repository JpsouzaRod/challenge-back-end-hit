using Microsoft.Extensions.Caching.Distributed;
using StarWarsChallenge.Domain.Application.Interface;

namespace StarWarsChallenge.Adapter.Redis.Service
{
    public class PlanetCache : IPlanetCache
    {   
        public PlanetCache(IDistributedCache cache) 
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromHours(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3),
            };

        }
     
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        public async Task<string?> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value, _options);
        }
    }
}
