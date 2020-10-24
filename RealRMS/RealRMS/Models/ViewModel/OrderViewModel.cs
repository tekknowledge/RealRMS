using System;
using System.Collections.Generic;

namespace RealRMS.Models.ViewModel
{
    public class OrderViewModel {
        public int EmployeeId { get; set; }

        public bool IsAdmin { get; set; }

        public OrderModel order { get; set; }

        public IEnumerable<MenuModel> fullMenu { get; set; }
    }
}