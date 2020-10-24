using System.Collections.Generic;

namespace RealRMS.Models.ViewModel {
    public class MenuListViewModel {
        public IEnumerable<MenuCategoryModel> MenuCategories { get; set; }

        public IEnumerable<MenuModel> MenuItems { get; set; }
    }
}