using System;
using RealRMS.Utility.Attributes;

namespace RealRMS.Models
{
    public class TimeCardModel : IRMSEntity {

        [DatabaseColumnName("Emp_Id")]
        public int EmployeeId { get; set; }

        public DateTime? In { get; set; }

        public DateTime? Out { get; set; }

        public int Id { get; set; }
    }
}