using System;
using System.Collections.Generic;

namespace trwm.Source.Infrastructure
{
    public class CachedKeyValueResolver<TKey, TValue>
    {
        private readonly Func<TKey, TValue> _resolver;
        private readonly Dictionary<TKey, TValue> _cache;

        public CachedKeyValueResolver(Func<TKey, TValue> resolverFunc)
        {
            _resolver = resolverFunc;
            _cache = new Dictionary<TKey, TValue>();
        }

        public TValue Get(TKey key)
        {
            return Resolve(key);
        }

        private TValue Resolve(TKey key)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = _resolver(key);
            }

            return _cache[key];
        }
    }
}