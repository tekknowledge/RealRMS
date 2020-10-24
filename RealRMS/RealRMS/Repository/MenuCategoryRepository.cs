using System;
using System.Collections.Generic;
using RealRMS.Models;
using RealRMS.Utility;

namespace RealRMS.Repository {
    public class MenuCategoryRepository : RepositoryWithCache<MenuCategoryModel> {
        public const string MENU_CATEGORY_GETALL = "menuCategory_getAll";
        public MenuCategoryRepository(ICacheWrapper cache, IConnectionFactory factory = null, ISqlWorker broker = null) : base(cache, factory, broker) { }
        public override bool Create(MenuCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public override bool Update(MenuCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public override MenuCategoryModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<MenuCategoryModel> GetAll() {
            broker.SetupCommand(MENU_CATEGORY_GETALL);
            return broker.GetObject<MenuCategoryModel>();
        }
    }
}