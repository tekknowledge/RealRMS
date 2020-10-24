using System;
using RealRMS.Models;
using RealRMS.Utility;

namespace RealRMS.Repository {
    public abstract class RepositoryWithCache<T> : Repository<T> where T : IRMSEntity {
        protected readonly ICacheWrapper cache;

        protected RepositoryWithCache(ICacheWrapper cache, IConnectionFactory factory = null, ISqlWorker broker = null) : base(factory, broker) {
            if (cache == null)
                throw new ArgumentNullException("cache");
            this.cache = cache;
        }

        protected virtual T GetFromCache<T>(string cacheKey) {
            var objectFromSession = cache.Get(cacheKey);
            return objectFromSession == null ? default(T) : (T)objectFromSession;
        }

        protected virtual void SetCache(string cacheKey, IRMSEntity model) {
            cache.Set(cacheKey, model);
        }
    }
}