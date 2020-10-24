using System.Collections.Generic;
using System.Web.Mvc;
using RealRMS.Models;
using RealRMS.Repository;
using System.Linq;
using RealRMS.Filters;
using RealRMS.Models.ViewModel;

namespace RealRMS.Controllers
{
    public class TransactionController : BaseController
    {
        public class CheckPartition {
            public int Partition { get; set; }
            public int[] Seats { get; set; }
            public int OrderId { get; set; }
        }

        [HttpPost]
        [LoginRequired]
        public ActionResult Create(IEnumerable<CheckPartition> partitons) {
            JsonResult json = new JsonResult();
            OrderRepository repo = new OrderRepository(false, loggedInUser.Id, applicationCache);
            CheckPartition[] splits = partitons.ToArray();
            string orderIds = string.Empty;
            int oldOrderId = splits[0].OrderId;
            OrderModel oldOrderModel = repo.Get(oldOrderId);
            TransactionModel transaction;
            TransactionRepository transactionRepo = new TransactionRepository();
            int tableId = 0;
            int employeeId = 0;
            IList<OrderItemModel> orderItems;
            closeoutOldOrder(oldOrderModel, splits.Length > 1, out tableId, out employeeId, out orderItems);
            if (splits.Length > 1) {
                foreach (var part in splits) {
                    if (part.Seats.Count() < 1)
                        continue;

                    var newOrderItems = orderItems.Where(item => part.Seats.Contains(item.SeatNumber));
                    foreach (var itm in newOrderItems) {
                        itm.OrderId = part.OrderId;
                    }
                    OrderModel model = new OrderModel();
                    model.OrderItems = newOrderItems.ToList();
                    model.InProgress = false;
                    model.NumberOfSeats = part.Seats.Length;
                    model.Ready = true;
                    model.TableId = oldOrderModel.TableId;
                    model.EmployeeId = oldOrderModel.EmployeeId;
                    model.Voided = false;
                    repo.Save(model);
                    repo.UpdateAndClose(model); // Ick, 2 database calls here...what the heck.
                    orderIds = orderIds + "," + model.Id.ToString();
                    transaction = new TransactionModel();
                    transaction.OrderId = model.Id;
                    transactionRepo.Save(transaction);
                }
                orderIds = orderIds.Substring(1);
            } else {
                orderIds = oldOrderId.ToString();
                transaction = new TransactionModel();
                transaction.OrderId = oldOrderModel.Id;
                transactionRepo.Save(transaction);
            }

            json.Data = orderIds;
            return json;
        }


        [HttpGet]
        [LoginRequired]
        public ActionResult ViewTransaction(string orderIds) {
            if (!HasAdminRole() && !HasServerRole())
                return View("~/Views/NoPermission.cshtml");

            string[] ids = orderIds.Split(',');
            IList<TransactionViewModel> vm = new List<TransactionViewModel>();

            OrderRepository orderRepo = new OrderRepository(false, loggedInUser.Id, applicationCache);
            MenuRepository menuRepo = new MenuRepository(applicationCache);
            foreach (var id in ids) {
                var transactionItem = new TransactionViewModel();
                OrderModel order = orderRepo.Get(int.Parse(id.Trim()));
                List<MenuModel> menuItems = new List<MenuModel>();
                foreach (var item in order.OrderItems) {
                    menuItems.Add(menuRepo.Get(item.MenuId));
                }
                transactionItem.MenuItems = menuItems;
                transactionItem.OrderId = order.Id;
                //transactionItem.TransactionId
                vm.Add(transactionItem);
            }

            return View("~/Views/Transaction.cshtml", vm);

        }
        private void closeoutOldOrder(OrderModel oldOrder, bool voidOrder, out int TableId, out int EmployeeId, out IList<OrderItemModel> orderItems) {
            OrderRepository repo = new OrderRepository(false, loggedInUser.Id, applicationCache);
            TableId = oldOrder.TableId;
            EmployeeId = oldOrder.EmployeeId;
            orderItems = oldOrder.OrderItems;
            oldOrder.Voided = voidOrder;
            repo.UpdateAndClose(oldOrder);
        }

    }
}
