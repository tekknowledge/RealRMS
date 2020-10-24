using System.Data;
using System.Data.SqlClient;

namespace RealRMS.Utility {
    public class DatabaseConnectionFactory : IConnectionFactory {
        private static readonly DatabaseConnectionFactory instance = new DatabaseConnectionFactory();

        static DatabaseConnectionFactory() {}

        private DatabaseConnectionFactory() { }

        public static DatabaseConnectionFactory Instance {
            get {
                return instance;
            }
        }

        public IDbConnection GetConnection(string connectionAlias) {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[connectionAlias].ConnectionString);
        }
    }
}
