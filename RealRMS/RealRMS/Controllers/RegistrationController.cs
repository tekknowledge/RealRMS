using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealRMS.Models;
using RealRMS.Repository;
using Newtonsoft.Json;
using RealRMS.Filters;

namespace RealRMS.Controllers {
    public class RegistrationController : BaseController {
        //
        // GET: /Registration/

        [HttpGet]
        public ActionResult New() {
            if ( !FirstTimeSetup() && !HasAdminRole())
                return View("~/Views/Shared/Error.cshtml"); //Todo: Replace this with "you don't have permission"

            return View("/Views/Registration/User.cshtml", new UserInfoModel());
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult Index() {
            UserInfoRepository r = new UserInfoRepository();
           
            return View("~/Views/Employees.cshtml", r.GetAll());
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult Edit(int id) {
            UserInfoRepository repo = new UserInfoRepository();
            UserInfoModel model = repo.Get(id);
            return View("~/Views/Registration/User.cshtml", model);
        }

        [HttpPost]
        public ActionResult Create(UserInfoModel model) {
            if (model.ConfirmPassword != model.Password)
                ModelState.AddModelError("Password", "Your passwords must match");
            if (model.Password.Length < 5)
                ModelState.AddModelError("Password", "Password must be at least 5 characters");
            if (model.Password.Length > 30)
                ModelState.AddModelError("Password", "Password must be between 5 and 30 characters");
            if (!ModelState.IsValid) {
                model.ValidationErrors = GetValidationErrors(ModelState);
                return View("~/Views/Registration/User.cshtml", model);
            }

            UserInfoRepository repo = new UserInfoRepository();
            model.Roles = JsonConvert.DeserializeObject<Role[]>(model.RoleString);
            repo.Save(model);
            return RedirectToAction("Edit", new {id = model.Id, Message = "Your employee was successfully created."});
        }

        [HttpPost]
        [LoginRequired]
        public ActionResult Update(UserInfoModel model) {
            ModelState["Password"].Errors.Clear();
            ModelState["ConfirmPassword"].Errors.Clear();
             if (!ModelState.IsValid) {
                model.ValidationErrors = GetValidationErrors(ModelState);
                return View("~/Views/Registration/User.cshtml", model);
            }

            UserInfoRepository repo = new UserInfoRepository();
            model.Roles = JsonConvert.DeserializeObject<Role[]>(model.RoleString);
            repo.Save(model);
            return RedirectToAction("Edit", new {id = model.Id, Message = "Your employee was successfully created."});           
        }

        private bool FirstTimeSetup() {
            return HttpUtility.UrlDecode(requestWrapper.QueryString("HiddenSiteKey")) == ConfigurationManager.AppSettings["SiteKey"] && !UsersExist();
        }

        private bool UsersExist() {
            UserInfoRepository repo = new UserInfoRepository();
            return repo.GetAll().Any();
        }
    }
}
