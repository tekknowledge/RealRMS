using System.Data;
using System.Data.SqlClient;

namespace RealRMS.Utility {
    public interface IConnectionFactory {
        IDbConnection GetConnection(string connectionAlias);
    }

    public class FakeConnectionFactory : IConnectionFactory {
        public IDbConnection GetConnection(string alias) {
            return new SqlConnection();
        }
    }
}