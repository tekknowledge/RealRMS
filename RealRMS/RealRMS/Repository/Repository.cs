using System;
using System.Collections.Generic;
using System.Data;
using RealRMS.Models;
using RealRMS.Utility;

namespace RealRMS.Repository {
    public abstract class Repository<T> : IRepository<T> where T : IRMSEntity {

        protected Repository(IConnectionFactory factory = null, ISqlWorker broker = null) {
            connectionFactory = factory ?? DatabaseConnectionFactory.Instance;
            this.broker = broker ?? new SqlWorker(connectionFactory.GetConnection(CONNECTION_STRING));
        }

        protected ISqlWorker broker;
        protected IConnectionFactory connectionFactory;

        protected const string CONNECTION_STRING = "ProductionConnection";

        public virtual bool Save(T model) {
            if (model.Id != default(int))
                return Update(model);
            return Create(model);
        }

        public abstract bool Create(T model);

        public abstract bool Update(T model);

        // Don't want to put this everywhere for now. Kinda in a rush.
        public virtual bool Delete(int id) {
            throw new NotImplementedException();
        }
        public abstract T Get(int id);

        public abstract IEnumerable<T> GetAll();

        protected virtual IDbConnection CreateConnection() {
            return connectionFactory.GetConnection(CONNECTION_STRING);
        }

        protected int ConvertToBit(bool value) {
            return value ? 1 : 0;
        }

        protected int ConvertToBit(bool? value) {
            return value.HasValue && value.Value ? 1 : 0;
        }
    }
}