using System;
using System.Data;

namespace RealRMS.Tests
{

    public class FakeDbConnection : IDbConnection
    {


        public void Dispose() {
            Dispose();
        }

        public void Close() {

        }
        public IDbTransaction BeginTransaction() {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il) {
            throw new NotImplementedException();
        }



        public void ChangeDatabase(string databaseName) {
            throw new NotImplementedException();
        }

        public IDbCommand CreateCommand() {
            throw new NotImplementedException();
        }

        public void Open() {
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; private set; }
        public string Database { get; private set; }
        public ConnectionState State { get; private set; }
    }
}
