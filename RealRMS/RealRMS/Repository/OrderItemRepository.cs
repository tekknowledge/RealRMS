using System.Collections.Generic;
using System.Linq;
using RealRMS.CacheKeyConstants;
using RealRMS.Models;
using RealRMS.Utility;
using RealRMS.Utility.Extension;

namespace RealRMS.Repository {
    public class OrderItemRepository : RepositoryWithCache<OrderItemModel> {
        private readonly int _orderId;

        public const string ORDER_GET_ALL = "orderItem_getAll";
        public const string ORDER_GET = "orderItem_get";
        public const string ORDER_UPDATE = "orderItem_update";
        public const string ORDER_INSERT = "orderItem_insert";
        public const string ORDER_DELETE = "orderItem_delete";
        public const string CACHE_KEY = "orderItem.{0}";

        public OrderItemRepository(int orderId, ICacheWrapper cache, IConnectionFactory factory = null, ISqlWorker broker = null) : base(cache, factory, broker) {
            _orderId = orderId;
        }

        public OrderItemRepository(ICacheWrapper cache, IConnectionFactory factory = null, ISqlWorker broker = null) : base(cache, factory, broker) {

        }

        public override bool Create(OrderItemModel model) {
            return Create(model, true);
        }


        public bool SaveOrderItems(IEnumerable<OrderItemModel> orderItems, out int orderId) {
            orderId = 0;
            if (orderItems == null)
                return false;

            int i = 0;
            bool saved = true;
            foreach (var item in orderItems) {
                if (i == 0)
                    orderId = item.OrderId;

                if (item.Id > 0)
                    saved = saved && Update(item, false);
                else
                    saved = saved && Create(item, false);
                i++;
            }
            ClearOrderCache(orderId);
            return saved;
        }

        public override bool Update(OrderItemModel model) {
            return Update(model, true);

        }

        public override bool Delete(int id) {
            broker.SetupCommand(ORDER_DELETE);
            broker.AddInputParameter("@id", id);
            bool deleted = broker.ExecuteNonQuery().SafeToInt() > 0;
            var cachedItem = cache.Get(ApplicationCacheConstants.GetOrderItemCacheKey(id)) as OrderItemModel;
            if (deleted && cachedItem != null) {
                ClearOrderCache(cachedItem.OrderId);
                cache.Remove(ApplicationCacheConstants.GetOrderItemCacheKey(id));
            }
            return deleted;
        }

        public bool Delete(OrderItemModel model) {
            return Delete(model.Id);
        }

        private bool Update(OrderItemModel model, bool clearOrderCache) {
            broker.SetupCommand(ORDER_UPDATE);
            broker.AddInputParameter("@id", model.Id);
            broker.AddInputParameter("@comment", model.Comment);
            broker.AddInputParameter("@seatNumber", model.SeatNumber);
            broker.AddInputParameter("@menuId", model.MenuId);
            if (model.Ready.HasValue)
                broker.AddInputParameter("@ready", model.Ready.Value, dbType: System.Data.DbType.Boolean);
            bool updated = broker.ExecuteNonQuery() > 0;
            if (updated)
                cache.Set(ApplicationCacheConstants.GetOrderItemCacheKey(model.Id), model);
            if (clearOrderCache)
                ClearOrderCache(model.Id);
            return updated;
        }

        private bool Create(OrderItemModel model, bool clearOrderCache) {
            broker.SetupCommand(ORDER_INSERT);
            broker.AddInputParameter("@comment", model.Comment);
            broker.AddInputParameter("@seatNumber", model.SeatNumber);
            broker.AddInputParameter("@menuId", model.MenuId);
            broker.AddInputParameter("@orderId", model.OrderId);
            model.Id = broker.ExecuteScalar().SafeToInt();
            if (model.Id > 0)
                cache.Set(ApplicationCacheConstants.GetOrderItemCacheKey(model.Id), model);
            if (clearOrderCache)
                ClearOrderCache(model.Id);
            return model.Id > 0;
        }

        private void ClearOrderCache(int orderId) {
            cache.Remove(ApplicationCacheConstants.GetOrderCacheKey(orderId));

        }
        public override OrderItemModel Get(int id) {
            var model = cache.Get(ApplicationCacheConstants.GetOrderItemCacheKey(id)) as OrderItemModel;
            if (model != null)
                return model;
            broker.SetupCommand(ORDER_GET);
            broker.AddInputParameter("@id", id);
            model = broker.GetObject<OrderItemModel>().First();
            cache.Set(ApplicationCacheConstants.GetOrderItemCacheKey(id), model);
            return model;
        }

        public override IEnumerable<OrderItemModel> GetAll() {
            broker.SetupCommand(ORDER_GET_ALL);
            broker.AddInputParameter("@orderId", _orderId);
            return broker.GetObject<OrderItemModel>();
        }
    }
}