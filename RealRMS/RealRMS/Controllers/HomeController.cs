using System;
using System.Configuration;
using System.Web.Mvc;
using RealRMS.Filters;
using RealRMS.Models;
using RealRMS.Models.ViewModel;
using RealRMS.Repository;

namespace RealRMS.Controllers {
    public class HomeController : BaseController {

        // GET: /Home/
        protected const string CONNECTION_STRING = "ProductionConnection";


        [HttpGet]
        public ActionResult Error() {
            return View("/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public ActionResult CauseError() {
            var i = 0;
            var x = 7 / i;
            return null;
        }

        [HttpGet]
        public ActionResult Index() {
            string domainName = System.Environment.UserDomainName;
            return View("/Views/Home.cshtml");
        }

        [HttpGet]
        public ActionResult Initialize(int id, string siteKey) {
            const string IP_ADDRESS_ENTRY = "REMOTE_ADDR";
            if (siteKey == ConfigurationManager.AppSettings["SiteKey"])
                return View("/Views/SiteInit.cshtml");

            return View("/Views/InitError.cshtml");
        }

        [HttpPost]
        public ActionResult Login() {
            UserInfoRepository repo = new UserInfoRepository();
            int id = int.Parse(requestWrapper.Form("Id"));
            string password = requestWrapper.Form("Password");

            if (repo.Login(id, password)) {
                sessionWrapper.Set(ME, repo.Get(id));
                Session.Timeout = 60;
                return RedirectToAction("Menu", "Home");
            }
            return RedirectToAction("Index", "Home", new { ErrorMessage = "Invalid credentials. Try to login again or contact the site administrator."});
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult Menu() {
            MainMenuViewModel vm = new MainMenuViewModel();
            TimeCardModel model = new TimeCardRepository(sessionWrapper).Get(loggedInUser.Id);
            vm.IsAdmin = HasAdminRole();
            vm.IsServer = HasServerRole();
            vm.IsRunner = HasRunnerRole();
            vm.IsCook = HasCookRole();
            vm.UserId = loggedInUser.Id;
            vm.IsClockedIn = model.In != null && model.Out == null;
            vm.TimeCardId = model.Id;
           
            return View("/Views/MainMenu.cshtml", vm);
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult Logout() {
            Session.Clear();
            return RedirectToAction("Index", new {message = "You have been logged out"});
        }

        [HttpPost]
        [LoginRequired]
        public ActionResult ClockIn(int EmployeeId) {
            TimeCardModel model = new TimeCardModel();
            model.EmployeeId = EmployeeId;
            model.In = DateTime.Now;
            model.Out = null;
            new TimeCardRepository(sessionWrapper).Save(model);
            JsonResult result = new JsonResult();
            result.Data = model;
            return result;
        }

        [HttpPost]
        [LoginRequired]
        public ActionResult ClockOut(int EmployeeId) {
            TimeCardRepository repo = new TimeCardRepository(sessionWrapper);
            TimeCardModel model = repo.Get(EmployeeId);
            new TimeCardRepository(sessionWrapper).Save(model);
            JsonResult result = new JsonResult();
            result.Data = model;
            return result;
        }
    }
}
