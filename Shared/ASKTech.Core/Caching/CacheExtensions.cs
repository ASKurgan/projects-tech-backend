using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Core.Caching
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddDistributedCache(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                string connection = configuration.GetConnectionString("Redis")
                                    ?? throw new ArgumentNullException(nameof(connection));

                options.Configuration = connection;
            });

            services.AddSingleton<ICacheService, DistributedCacheService>();

            return services;
        }
    }
}
