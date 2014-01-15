using System;
using System.Collections.Generic;
using System.Reflection;
using Neddle.Caching;
using Neddle.Extensions;

namespace Neddle.Web.Services
{
    /// <summary>
    /// A REST web service that caches data.
    /// </summary>
    /// <typeparam name="T">Type of entities that will be cached by this service.</typeparam>
    public abstract class CachedService<T> : Service where T : class
    {
        private ICacheProvider _cacheProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedService{T}"/> class.
        /// </summary>
        /// <param name="cacheProvider">The caching strategy. Passing null will force use of <see cref="NullCacheProvider"/>.</param>
        protected CachedService(ICacheProvider cacheProvider)
        {
            if (null == cacheProvider)
            {
                cacheProvider = new NullCacheProvider();
            }
            _cacheProvider = cacheProvider;
        }

        /// <summary>
        /// Gets a list of entities having type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A list of entities, if they exist.</returns>
        public abstract List<T> Get();

        protected virtual TV TryGetFromCacheAddIfMissing<TV>(MethodBase callingMethod, Func<TV> query, params object[] keyHints) where TV : class
        {
            callingMethod.CheckNull("callingMethod");
            query.CheckNull("query");

            // TODO: KLC hook this up to cachestrategy
            throw new NotImplementedException();

            //// doing this always for better test coverage in implementers
            //string cacheKey = BuildCacheKey(callingMethod, keyHints);

            //if (null == HttpContext.Current || null == HttpContext.Current.Cache)
            //{
            //    Log.WarnFormat(Resources.log_cacheUnavailable, typeof(T).Name);
            //    return query.Invoke();
            //}

            //if (SuppressCaching)
            //{
            //    Log.Info(Resources.log_cacheDisabled);
            //    return query.Invoke();
            //}

            //TV maybeFromCache = (TV)HttpContext.Current.Cache.Get(cacheKey);
            //if (null != maybeFromCache)
            //{
            //    return maybeFromCache;
            //}

            //lock (Lock)
            //{
            //    // cache was empty before we got the lock, check again inside the lock
            //    maybeFromCache = (TV)HttpContext.Current.Cache.Get(cacheKey);
            //    if (null != maybeFromCache)
            //    {
            //        return maybeFromCache;
            //    }

            //    maybeFromCache = query.Invoke();
            //    if (null != maybeFromCache)
            //    {
            //        HttpContext.Current.Cache.Insert(cacheKey, maybeFromCache);
            //    }
            //}

            //return maybeFromCache;
        }


        /// <summary>
        /// Builds the cache key based on the name of the method called and the specified key hints.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="keyHints">The key hints.</param>
        /// <returns>A string contaiing a cache key.</returns>
        protected virtual string BuildCacheKey(MethodBase methodInfo, params object[] keyHints)
        {
            methodInfo.CheckNull("methodInfo");

            return string.Format("{0}::{1}", GetType().FullName, methodInfo.Name);
        }
    }
}