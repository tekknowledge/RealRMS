using System.Collections.Generic;

namespace RealRMS.Utility {
    public class MemoryCache : ICacheWrapper {
        public static Dictionary<string, object> _items = new Dictionary<string, object>();
        public object Get(string key) {
            if (_items.ContainsKey(key))
                return _items[key];
            return null;
        }

        public void Set(string key, object value) {
            _items[key] = value;
        }

        public void Remove(string key) {
            _items.Remove(key);
        }
    }
}