using System.Web.Mvc;
using Newtonsoft.Json;
using RealRMS.Filters;
using RealRMS.Models;
using RealRMS.Models.ViewModel;
using RealRMS.Repository;

namespace RealRMS.Controllers
{
    public class MenuController : BaseController
    {

        [HttpGet]
        [LoginRequired]
        public ActionResult Index() {
            MenuCategoryRepository categoryRepo = new MenuCategoryRepository(memoryCache);
            categoryRepo.GetAll();
            MenuRepository menuRepo = new MenuRepository(applicationCache);
            menuRepo.GetAll();
            MenuListViewModel vm = new MenuListViewModel();
            vm.MenuItems = menuRepo.GetAll();
            vm.MenuCategories = categoryRepo.GetAll();
           

            return View("~/Views/MenuItems.cshtml", vm);
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult New() {
            return View("~/Views/MenuItem.cshtml", new MenuModel());
        }

        [HttpPost]
        [LoginRequired]
        public ActionResult Create(MenuModel model) {
            if (!ModelState.IsValid) {
                model.ValidationErrors = GetValidationErrors(ModelState);
                return View("~/Views/MenuItem.cshtml", model);
            }

            MenuRepository repo = new MenuRepository(applicationCache);
            repo.Save(model);
            return RedirectToAction("Edit", new { id = model.Id, Message = "Your menu item was successfully created." });
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult Edit(int id) {
            MenuRepository repo = new MenuRepository(applicationCache);
            return View("~/Views/MenuItem.cshtml", repo.Get(id));

        }

        [HttpPost]
        [LoginRequired]
        public ActionResult Update(MenuModel model) {
             if (!ModelState.IsValid) {
                model.ValidationErrors = GetValidationErrors(ModelState);
                return View("~/Views/MenuItem.cshtml", model);
            }

            MenuRepository repo = new MenuRepository(applicationCache);
            repo.Save(model);
            return RedirectToAction("Edit", new {id = model.Id, Message = "Your menu item was successfully modified."});           
        }

        [HttpDelete]
        public ActionResult Delete(int id) {
            MenuRepository repo = new MenuRepository(applicationCache);
            bool success = repo.Delete(id);
            JsonResult result = new JsonResult();
            result.Data = success;
            return result;
        }
    }
}
