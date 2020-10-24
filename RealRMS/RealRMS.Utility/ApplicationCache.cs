using System.Web;

namespace RealRMS.Utility {
    public class ApplicationCache : ICacheWrapper {
        private readonly HttpApplicationState _application;

        public ApplicationCache(HttpApplicationState application) {
            _application = application;
        }
        public object Get(string key) {
            return _application[key];
        }

        public void Set(string key, object value) {
            _application[key] = value;
        }

        public void Remove(string key) {
            _application.Remove(key);
        }        
    }
}