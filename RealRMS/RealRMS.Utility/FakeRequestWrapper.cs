using System.Collections.Generic;

namespace RealRMS.Utility {
    public class FakeRequestWrapper : IRequestWrapper {
        private readonly IDictionary<string, string> _form;
        private readonly IDictionary<string, string> _queryString;

        public FakeRequestWrapper(IDictionary<string, string> form, IDictionary<string, string> queryString) {
            _form = form ?? new Dictionary<string, string>();
            _queryString = queryString ?? new Dictionary<string, string>();

        }
        public string QueryString(string key) {
            return _queryString.ContainsKey(key) ? _queryString[key] : null;
        }

        public string Form(string key) {
            return _form.ContainsKey(key) ? _form[key] : null;
        }
    }
}