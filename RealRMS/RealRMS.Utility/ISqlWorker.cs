using System.Collections.Generic;
using System.Data;

namespace RealRMS.Utility {
    public interface ISqlWorker {
        IDbCommand Cmd { set; }

        void SetupCommand(string SQL, CommandType commandType = CommandType.StoredProcedure);

        void AddInputParameter(string parameterName, object parameterValue, SqlCollectionType collectionType = SqlCollectionType.None, DbType dbType = DbType.AnsiString);

        void AddOutputParameter(string parameterName);

        T ReturnOutputParameter<T>();

        IDataReader GetDataReader();

        IEnumerable<T> GetObject<T>();

        int ExecuteNonQuery();

        object ExecuteScalar();
    }
}
