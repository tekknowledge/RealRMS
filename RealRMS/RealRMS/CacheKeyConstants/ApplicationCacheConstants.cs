using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealRMS.CacheKeyConstants
{
    public static class ApplicationCacheConstants {
        public const string MENU_ITEM_CACHE_KEY = "MenuItem.{0}";
        public const string ORDER_CACHE_KEY = "Order.{0}";
        public const string ORDER_ITEM_CACHE_KEY = "OrderItem.{0}";

        public static string GetMenuItemCacheKey(int id) {
            return string.Format(MENU_ITEM_CACHE_KEY, id);
        }

        public static string GetOrderItemCacheKey(int id) {
            return string.Format(ORDER_ITEM_CACHE_KEY, id);
        }

        public static string GetOrderCacheKey(int id) {
            return string.Format(ORDER_CACHE_KEY, id);
        }
    }
}