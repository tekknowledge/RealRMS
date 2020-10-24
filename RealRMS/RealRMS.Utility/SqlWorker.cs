using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace RealRMS.Utility {
    public class SqlWorker : BaseSqlWorker {

        #region constructor

        private Stopwatch _diagnostic;

        public SqlWorker(IDbConnection connection) : base(connection) {
            _diagnostic = new Stopwatch();
        }

        #endregion

        public override void SetupCommand(string query, CommandType commandType) {
            if (Cmd.Connection.ConnectionString == string.Empty)
                Cmd.Connection.ConnectionString = ConnectionString;
            IDbConnection conn = Cmd.Connection;
            Cmd = new SqlCommand();
            Cmd.Connection = conn;
            Cmd.CommandText = query;
            Cmd.CommandType = commandType;
        }

        public override void AddInputParameter(string parameterName, object parameterValue, SqlCollectionType collectionType = SqlCollectionType.None, DbType type = DbType.AnsiString) {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = FormatForSql(parameterName);
            parameter.Value = parameterValue;
            if (collectionType != SqlCollectionType.None) {
                parameter.TypeName = collectionType.ToString();
                parameter.SqlDbType = SqlDbType.Structured;
            }
            Cmd.Parameters.Add(parameter);
        }

        
        public override void AddOutputParameter(string parameterName) {
            SqlParameter param = new SqlParameter();
            param.ParameterName = FormatForSql(parameterName);
            param.Direction = ParameterDirection.Output;
            Cmd.Parameters.Add(param);
        }

        public override T ReturnOutputParameter<T>() {
            SqlParameter parameterToFind = Cmd.Parameters.Cast<SqlParameter>().FirstOrDefault(p => p.Direction == ParameterDirection.Output);
            return parameterToFind == null ? default(T) : (T)parameterToFind.Value;
        }

        public override IDataReader GetDataReader() {
            OpenConnection();
            return Cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public override int ExecuteNonQuery() {
            int rowsAffected;
            using (Cmd.Connection) {
                OpenConnection();
                rowsAffected = Cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return rowsAffected;
        }

        public override object ExecuteScalar() {
            object result;
            using (Cmd.Connection) {
                OpenConnection();
                result = Cmd.ExecuteScalar();
                CloseConnection();
            }
            return result;
        }
    }
}