using System.Data;
using System.Data.SqlClient;

namespace RealRMS.Utility {
    public class FakeSqlCommand : IDbCommand {
        public void Dispose() {
            throw new System.NotImplementedException();
        }

        public void Prepare() {
            throw new System.NotImplementedException();
        }

        public void Cancel() {
            throw new System.NotImplementedException();
        }

        public IDbDataParameter CreateParameter() {
            throw new System.NotImplementedException();
        }

        public int ExecuteNonQuery() {
            throw new System.NotImplementedException();
        }

        public IDataReader ExecuteReader() {
            throw new System.NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior) {
            throw new System.NotImplementedException();
        }

        public object ExecuteScalar() {
            throw new System.NotImplementedException();
        }

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDataParameterCollection Parameters { get; private set; }
        public UpdateRowSource UpdatedRowSource { get; set; }
    }
    public class FakeSqlConnection : IDbConnection {
        public void Dispose() {
            Dispose();
        }

        public IDbTransaction BeginTransaction() {
            throw new System.NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il) {
            throw new System.NotImplementedException();
        }

        public void Close() {
            State = ConnectionState.Closed;
        }

        public void ChangeDatabase(string databaseName) {
            throw new System.NotImplementedException();
        }

        public IDbCommand CreateCommand() {
            return new SqlCommand();
        }

        public void Open() {
            State = ConnectionState.Open;
        }

        public string ConnectionString { get; set; }

        public int ConnectionTimeout { get; private set; }

        public string Database { get; private set; }

        public ConnectionState State { get; private set; }
    }
}
