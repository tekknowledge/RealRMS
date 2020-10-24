using System.Web.SessionState;

namespace RealRMS.Utility {
    public class SessionCacheWrapper : ICacheWrapper {
        private readonly HttpSessionState _session;
        public SessionCacheWrapper(HttpSessionState session) {
            _session = session;
        }
        public object Get(string key) {
            return _session[key];
        }

        public void Set(string key, object value) {
            _session[key] = value;
        }

        public void Remove(string key) {
            _session.Remove(key);
        }
    }
}