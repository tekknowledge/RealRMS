using System.Collections.Generic;
using System.Web.Mvc;
using RealRMS.Models;
using RealRMS.Utility;
using System.Linq;
namespace RealRMS.Controllers {
    public class BaseController : Controller {
        protected readonly ICacheWrapper sessionWrapper;
        protected readonly ICacheWrapper applicationCache;
        protected readonly ICacheWrapper memoryCache;
        protected readonly IRequestWrapper requestWrapper;

        public const string ME = "me";

        private const int ADMIN_ROLE = 1;
        private const int SERVER_ROLE = 2;
        private const int COOK_ROLE = 4;
        private const int RUNNER_ROLE = 3;


        private UserInfoModel _loggedInUser;

        protected UserInfoModel loggedInUser {
            get {
                if (_loggedInUser == null) {
                    _loggedInUser = sessionWrapper.Get(ME) as UserInfoModel;
                }
                return _loggedInUser;
            }
        }
      
        public BaseController() {
            sessionWrapper = new SessionCacheWrapper(System.Web.HttpContext.Current.Session);
            requestWrapper = new RequestWrapper(System.Web.HttpContext.Current.Request);
            applicationCache = new ApplicationCache(System.Web.HttpContext.Current.Application);
            memoryCache = new MemoryCache();
        }

        public BaseController(ICacheWrapper sessionWrapper, ICacheWrapper applicationCache, ICacheWrapper memoryCache, IRequestWrapper requestWrapper) {
            this.sessionWrapper = sessionWrapper;
            this.requestWrapper = requestWrapper;
            this.applicationCache = applicationCache;
            this.memoryCache = memoryCache;
        }

        public bool IsLoggedIn() {
            return loggedInUser != null;
        }

        public bool HasAdminRole() {
            return IsLoggedIn() && loggedInUser.Roles.Any(role => role.Id == ADMIN_ROLE);
        }

        public bool HasServerRole() {
            return IsLoggedIn() && loggedInUser.Roles.Any(role => role.Id == SERVER_ROLE);
        }

        public bool HasCookRole() {
            return IsLoggedIn() && loggedInUser.Roles.Any(role => role.Id == COOK_ROLE);
        }

        public bool HasRunnerRole() {
            return IsLoggedIn() && loggedInUser.Roles.Any(role => role.Id == RUNNER_ROLE);
        }

        protected IEnumerable<ValidationError> GetValidationErrors(ModelStateDictionary modelState) {
            Stack<ValidationError> validationErrors = new Stack<ValidationError>();
            foreach (var key in modelState.Keys) {
                var entry = modelState[key];
                if (entry.Errors.Any()) {
                    validationErrors.Push(new ValidationError {HtmlId = key, Message = entry.Errors.First().ErrorMessage, Value = entry.Value.AttemptedValue});
                }
            }
            return validationErrors;
        } 
    }

}