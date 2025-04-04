﻿using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Core.Caching
{
    public interface ICacheService
    {
        Task<T?> GetOrSetAsync<T>(
            string key,
            DistributedCacheEntryOptions options,
            Func<Task<T?>> factory,
            CancellationToken cancellationToken = default)
            where T : class;

        Task<T?> GetAsync<T>(
            string key,
            CancellationToken cancellationToken = default)
            where T : class;

        Task SetAsync<T>(
            string key,
            T value,
            DistributedCacheEntryOptions options,
            CancellationToken cancellationToken = default)
            where T : class;

        Task RemoveAsync(string key, CancellationToken cancellationToken = default);

        Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);
    }
}
