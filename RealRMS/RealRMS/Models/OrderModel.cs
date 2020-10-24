using System.Collections.Generic;
using RealRMS.Utility.Attributes;

namespace RealRMS.Models
{
    public class OrderModel : IRMSValidatableEntity
    {
        public OrderModel() {
            OrderItems = new List<OrderItemModel>();
        }

        public int Id { get; set; }

        public int NumberOfSeats { get; set; }

        [DatabaseColumnName("EmpId")]
        public int EmployeeId { get; set; }

        public int TableId { get; set; }

        [NotInQuery]
        public IList<OrderItemModel> OrderItems { get; set; }

        [NotInQuery]
        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        [NotInQuery]
        public bool Ready { get; set; }

        public bool Delivered { get; set; }

        public bool InProgress { get; set; }
        [NotInQuery]
        public bool Voided { get; set; }
    }
}