using System;
using System.Collections.Generic;
using System.Linq;
using RealRMS.Models;
using RealRMS.Utility.Extension;

namespace RealRMS.Repository {
    public class TransactionRepository : Repository<TransactionModel> {
        public const string TRANSACTION_INSERT = "transaction_insert";
        public const string TRANSACTION_GET = "transaction_get";
     
        public override bool Create(TransactionModel model) {
            broker.SetupCommand(TRANSACTION_INSERT);
            broker.AddInputParameter("@OrderId", model.OrderId);
            model.Id = broker.ExecuteScalar().SafeToInt();
            return model.Id > 0;
        }

        public override bool Update(TransactionModel model) {
            throw new NotImplementedException();
        }

        public override TransactionModel Get(int id) {
            broker.SetupCommand(TRANSACTION_INSERT);
            broker.AddInputParameter("@Id", id);
            return broker.GetObject<TransactionModel>().FirstOrDefault();
        }

        public override IEnumerable<TransactionModel> GetAll() {
            throw new NotImplementedException();
        }
    }
}