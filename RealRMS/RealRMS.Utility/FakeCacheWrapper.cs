using System.Collections.Generic;

namespace RealRMS.Utility {
    public class FakeCacheWrapper : ICacheWrapper {
        private readonly IDictionary<string, object> _session;
        public FakeCacheWrapper() {
            _session = new Dictionary<string, object>();
        }

        public object Get(string key) {
            if (_session.ContainsKey(key))
                return _session[key];
            return null;
        }

        public void Set(string key, object value) {
            _session[key] = value;
        }

        public void Remove(string key) {
            if (_session.ContainsKey(key))
                _session.Remove(key);
        }
    }
}