using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealRMS.Models {
    public class TransactionModel : IRMSEntity{
        public int Id { get; set; }

        public int OrderId { get; set; }
    }
}