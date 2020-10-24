using System.Web;

namespace RealRMS.Utility {
    public class RequestWrapper : IRequestWrapper {
        private readonly HttpRequest _request;
        public RequestWrapper(HttpRequest request) {
            _request = request;
        }
        public string QueryString(string key) {
            return _request.QueryString[key];
        }

        public string Form(string key) {
            return _request.Form[key];
        }
    }
}