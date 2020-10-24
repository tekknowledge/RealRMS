using System.Web.Mvc;
using RealRMS.Controllers;
using RealRMS.Utility;

namespace RealRMS.Filters {
    public class LoginRequiredAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            if (filterContext.HttpContext.Session[BaseController.ME] == null) {
                filterContext.Result = new RedirectResult("/");
            }
                
        }
    }
}