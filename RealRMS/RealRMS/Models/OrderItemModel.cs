using System.Collections.Generic;

namespace RealRMS.Models
{
    public class OrderItemModel : IRMSEntity {
        public int Id { get; set; }

        public int SeatNumber { get; set; }

        public int MenuId { get; set; }

        public int OrderId { get; set; }

        public bool? Ready { get; set; }

        public string Comment { get; set; }
    }
}