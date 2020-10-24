using System.Collections.Generic;
using System.Web.Mvc;
using RealRMS.Filters;
using RealRMS.Models;
using RealRMS.Models.ViewModel;
using RealRMS.Repository;

namespace RealRMS.Controllers
{
    public class OrderController : BaseController
    {
        //
        // GET: /Order/

        [HttpGet]
        [LoginRequired]
        public ActionResult Index() {
            bool isWaiter = HasServerRole() && !HasAdminRole() && !HasRunnerRole() && !HasCookRole();
            OrderRepository repo = new OrderRepository(!isWaiter, loggedInUser.Id, applicationCache);
            ViewBag.CanTakeOrders = HasServerRole() || HasAdminRole();
            ViewBag.IsCook = HasCookRole() || HasAdminRole();
            return View("~/Views/Orders.cshtml", repo.GetAll());
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult New() {
            if (!HasServerRole() && !HasAdminRole())
                return View("~/Views/NoPermission.cshtml");
            MenuRepository repo = new MenuRepository(applicationCache);
            OrderViewModel vm = new OrderViewModel();
            vm.order = new OrderModel();
            vm.fullMenu = repo.GetAll();
            vm.EmployeeId = loggedInUser.Id;
            vm.IsAdmin = HasAdminRole();
            return View("~/Views/Order.cshtml", vm);
        }

        [HttpGet]
        [LoginRequired]
        public ActionResult Edit(int id) {
            if (!HasServerRole() && !HasAdminRole())
                return View("~/Views/NoPermission.cshtml");
            MenuRepository menuRepo = new MenuRepository(applicationCache);
            OrderRepository orderRepo = new OrderRepository(false, loggedInUser.Id, applicationCache);
            OrderViewModel vm = new OrderViewModel();
            vm.order = orderRepo.Get(id);
            vm.fullMenu = menuRepo.GetAll();
            vm.EmployeeId = loggedInUser.Id;
            vm.IsAdmin = HasAdminRole();

            return View("~/Views/Order.cshtml", vm);

        }

        [HttpPost]
        [LoginRequired]
        public ActionResult Create(OrderModel model) {
            JsonResult result = new JsonResult();
            OrderRepository orderRepo = new OrderRepository(false, model.EmployeeId, applicationCache);
            bool inserted = orderRepo.Save(model);
            if (inserted) {
                result.Data = model;
            } else {
                result.Data = new OrderModel();
            }
            return result;
        }


        [HttpPost]
        [LoginRequired]
        public ActionResult AddItems(IEnumerable<OrderItemModel> orderItems) {
            JsonResult result = new JsonResult();
            OrderItemRepository orderItemRepo = new OrderItemRepository(applicationCache);
            OrderRepository orderRepo = new OrderRepository(false, loggedInUser.Id, applicationCache);
            int orderId;
            orderItemRepo.SaveOrderItems(orderItems, out orderId);
            var order = orderRepo.Get(orderId);
            order.InProgress = true;
            order.Delivered = false;
            orderRepo.Save(order);
            result.Data = orderRepo.Get(orderId);
            return result;
        }

        [HttpDelete]
        [LoginRequired]
        public ActionResult DeleteItem(int id) {
            JsonResult result = new JsonResult();
            OrderItemRepository orderItemRepo = new OrderItemRepository(applicationCache);
            var model = orderItemRepo.Get(id);
            OrderRepository orderRepo = new OrderRepository(false, loggedInUser.Id, applicationCache);          
            result.Data = orderItemRepo.Delete(model);
            return result;
        }

        [HttpPost]
        [LoginRequired]
        public ActionResult SetDelivered(bool value, int orderid) {
            JsonResult result = new JsonResult();
            OrderRepository repo = new OrderRepository(false, loggedInUser.Id, applicationCache);
            OrderModel model = repo.Get(orderid);
            model.Delivered = value;
            result.Data = repo.Save(model);
            return result;
        }

        [HttpPost]
        [LoginRequired]
        public ActionResult Update(OrderModel model) {
            JsonResult result = new JsonResult();
            OrderRepository orderRepo = new OrderRepository(false, model.EmployeeId, applicationCache);
            result.Data = orderRepo.UpdateAndClose(model);
            return result;

        }

        [HttpPost]
        [LoginRequired]
        public ActionResult Finish() {
            throw new System.NotImplementedException();

        }
    }
}
