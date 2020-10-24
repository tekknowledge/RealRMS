using System.Collections.Generic;
using System.Data;
using System.Linq;
using RealRMS.CacheKeyConstants;
using RealRMS.Models;
using RealRMS.Utility;
using RealRMS.Utility.Extension;

namespace RealRMS.Repository {
    public class OrderRepository : RepositoryWithCache<OrderModel> {
        private readonly bool _isAdmin;
        private readonly int _employeeId;

        public const string ORDER_GET_ALL = "order_getAll";
        public const string ORDER_GET = "order_get";
        public const string ORDER_UPDATE = "order_update";
        public const string ORDER_INSERT = "order_insert";

        public OrderRepository(bool isAdmin, int employeeId, ICacheWrapper cache, IConnectionFactory factory = null, ISqlWorker broker = null) : base(cache, factory, broker) {
            _isAdmin = isAdmin;
            _employeeId = employeeId;
        }

        public override bool Create(OrderModel model) {
            broker.SetupCommand(ORDER_INSERT);
            broker.AddInputParameter("@NumberofSeats", model.NumberOfSeats);
            broker.AddInputParameter("@EmployeeId", model.EmployeeId);
            broker.AddInputParameter("@TableId", model.TableId);
            model.Id = broker.ExecuteScalar().SafeToInt();
            if (model.Id > 0)
                cache.Set(ApplicationCacheConstants.GetOrderCacheKey(model.Id), model);
            return model.Id > 0;
        }

        public override bool Update(OrderModel model) {
            return UpdateAndClose(model, false);
        }

        public bool UpdateAndClose(OrderModel model, bool close = true) {
            broker.SetupCommand(ORDER_UPDATE);
            broker.AddInputParameter("@id", model.Id);
            broker.AddInputParameter("@NumberOfSeats", model.NumberOfSeats);
            broker.AddInputParameter("@delivered", model.Delivered, dbType: DbType.Boolean);
            broker.AddInputParameter("@inprogress", model.InProgress, dbType: DbType.Boolean);
            broker.AddInputParameter("@voided", model.Voided, dbType: DbType.Boolean);
            broker.AddInputParameter("@close", close);

            bool updated = broker.ExecuteNonQuery() > 0;
            if (updated)
                cache.Set(ApplicationCacheConstants.GetOrderCacheKey(model.Id), model);
            return updated;
        }
        public override OrderModel Get(int id) {
            var order = cache.Get(ApplicationCacheConstants.GetOrderCacheKey(id)) as OrderModel;
            if (order != null)
                return order;
            broker.SetupCommand(ORDER_GET);
            broker.AddInputParameter("@id", id);
            order = new OrderModel();
            using (IDataReader rdr = broker.GetDataReader()) {
                while (rdr.Read()) {
                    order.Id = rdr["id"].SafeToInt();
                    order.EmployeeId = rdr["empId"].SafeToInt();
                    order.NumberOfSeats = rdr["NumberOfSeats"].SafeToInt();
                    order.TableId = rdr["tableId"].SafeToInt();
                    order.Delivered = rdr["delivered"].SafeToBool();
                    order.Voided = false;
                    order.InProgress = rdr["inProgress"].SafeToBool();
                    OrderItemRepository orderItemRepo = new OrderItemRepository(order.Id, cache);
                    order.OrderItems = orderItemRepo.GetAll().ToList();
                    order.Ready = order.OrderItems.All(oi => oi.Ready != null && oi.Ready.Value);
                }
            }
            cache.Set(ApplicationCacheConstants.GetOrderCacheKey(id), order);
            return order;
        }

        public override IEnumerable<OrderModel> GetAll() {
            broker.SetupCommand(ORDER_GET_ALL);
            if (!_isAdmin)
                broker.AddInputParameter("@employeeId", _employeeId);
            IEnumerable<OrderModel> orders = broker.GetObject<OrderModel>();
            
            foreach (var o in orders) {
                OrderItemRepository itemRepo = new OrderItemRepository(o.Id, cache);
                o.OrderItems = itemRepo.GetAll().ToList();
            }
            return orders;
        }

    }
}