using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer.Utilities
{
    public class RedisCacheHelper
    {
        public static T GetFromCache<T>(string cacheKey, IDistributedCache cache) where T : class
        {
            var cachedData = cache.Get(cacheKey);
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                return JsonSerializer.Deserialize<T>(cachedDataString);
            }
            return null;
        }

        public static void SetToCache<T>(string cacheKey, IDistributedCache cache, T data, int absoluteExpirationMinutes = 20, int slidingExpirationMinutes = 10)
        {
            try
            {
                var cachedDataString = JsonSerializer.Serialize(data);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                var options = new DistributedCacheEntryOptions()
                                  .SetAbsoluteExpiration(DateTime.Now.AddMinutes(absoluteExpirationMinutes))
                                  .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationMinutes));

                cache.Set(cacheKey, newDataToCache, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
