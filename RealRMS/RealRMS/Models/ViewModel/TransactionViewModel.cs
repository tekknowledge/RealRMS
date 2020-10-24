using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace RealRMS.Models.ViewModel
{
    public class TransactionViewModel {
        public IEnumerable<MenuModel> MenuItems;

        public int TransactionId { get; set; }

        public int OrderId { get; set; }
    }
}