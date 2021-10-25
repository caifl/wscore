using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Web.Caching
{
    public sealed class Cache : System.Collections.IEnumerable
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public static DateTime NoAbsoluteExpiration { get; internal set; }

        public static TimeSpan NoSlidingExpiration;

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public object Add(string key, object value, CacheDependency dependencies, 
            DateTime absoluteExpiration, TimeSpan slidingExpiration, 
            CacheItemPriority priority, 
            CacheItemRemovedCallback onRemoveCallback)
        {
            this.values.Add(key, value);

            return value;
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, 
            TimeSpan slidingExpiration, System.Web.Caching.CacheItemPriority priority, 
            System.Web.Caching.CacheItemRemovedCallback onRemoveCallback)
        {
            this.values[key] = value;
        }

        public object Get(string key)
        {
            object value = null;

            this.values.TryGetValue(key, out value);

            return value;
        }
    }

    public class CacheDependency
    {

    }

    public enum CacheItemPriority
    {
        NotRemovable
    }

    public delegate void CacheItemRemovedCallback();
}
