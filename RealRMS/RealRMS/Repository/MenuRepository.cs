using System;
using System.Collections.Generic;
using System.Data;
using RealRMS.CacheKeyConstants;
using RealRMS.Models;
using RealRMS.Utility;
using RealRMS.Utility.Extension;

namespace RealRMS.Repository {
    public class MenuRepository : RepositoryWithCache<MenuModel> {
        public const string MENU_GETALL = "menu_getAll";
        public const string MENU_CREATE = "menu_insert";
        public const string MENU_UPDATE = "menu_update";
        public const string MENU_GET = "menu_get";
        public const string MENU_DELETE = "menu_delete";
        public const string MENU_CACHE_KEY = "menuItem.{0}";

        public MenuRepository(ICacheWrapper cache, IConnectionFactory factory = null, ISqlWorker broker = null) : base(cache, factory, broker) {}

        public override bool Create(MenuModel model) {
            broker.SetupCommand(MENU_CREATE);
            broker.AddInputParameter("@price", model.Price);
            broker.AddInputParameter("@menucategoryid", model.MenuCategory.Id);
            broker.AddInputParameter("@name", model.Name);
            broker.AddInputParameter("@description", model.Description);
            model.Id = broker.ExecuteScalar().SafeToInt();
            bool inserted = model.Id > 0;
            if (inserted) {
                cache.Set(ApplicationCacheConstants.GetMenuItemCacheKey(model.Id), model);
            }
            return inserted;
        }

        public override bool Update(MenuModel model) {
            broker.SetupCommand(MENU_UPDATE);
            broker.AddInputParameter("@price", model.Price);
            broker.AddInputParameter("@menucategoryid", model.MenuCategory.Id);
            broker.AddInputParameter("@name", model.Name);
            broker.AddInputParameter("@description", model.Description);
            broker.AddInputParameter("@id", model.Id);
            bool updated = broker.ExecuteNonQuery().SafeToInt() > 0;
            if (updated) {
                cache.Set(ApplicationCacheConstants.GetMenuItemCacheKey(model.Id), model);
            }
            return updated;
        }

        public override MenuModel Get(int id) {
            string key = ApplicationCacheConstants.GetMenuItemCacheKey(id);
            MenuModel model = cache.Get(key) as MenuModel;
            if (model != null) {
                return model;
            }
            broker.SetupCommand(MENU_GET);
            broker.AddInputParameter("@id", id);
            using (IDataReader rdr = broker.GetDataReader()) {
                if (rdr.Read()) {
                    model = new MenuModel();
                    model.Id = id;
                    model.Name = rdr["name"].SafeToString();
                    model.Description = rdr["description"].SafeToString();
                    model.MenuCategory = new MenuCategoryModel();
                    model.MenuCategory.Name = rdr["CategoryName"].SafeToString();
                    model.MenuCategory.Id = rdr["CategoryId"].SafeToInt();
                    model.Price = double.Parse(rdr["price"].SafeToString());
                }
            }
            cache.Set(ApplicationCacheConstants.GetMenuItemCacheKey(model.Id), model);
            return model;
        }

        public override bool Delete(int id) {
            broker.SetupCommand(MENU_DELETE);
            broker.AddInputParameter("@id", id);
            bool deleted = broker.ExecuteNonQuery().SafeToInt() > 0;
            if (deleted) {
                cache.Remove(ApplicationCacheConstants.GetMenuItemCacheKey(id));
            }
            return deleted;
        }
        public override IEnumerable<MenuModel> GetAll() {
            broker.SetupCommand(MENU_GETALL);
            List<MenuModel> models = new List<MenuModel>();
            using (IDataReader rdr = broker.GetDataReader()) {
                while (rdr.Read()) {
                    MenuModel item = new MenuModel();
                    item.Id = rdr["id"].SafeToInt();
                    item.Name = rdr["name"].SafeToString();
                    item.Description = rdr["description"].SafeToString();
                    item.MenuCategory = new MenuCategoryModel() {Id = rdr["CategoryId"].SafeToInt(), Name = rdr["categoryName"].SafeToString()};
                    item.Price = double.Parse(rdr["Price"].SafeToString());
                    models.Add(item);
                }
            }
            return models;
        }
    }
}