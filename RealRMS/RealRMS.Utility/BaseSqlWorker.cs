using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using RealRMS.Utility.Attributes;
using RealRMS.Utility.Extension;

namespace RealRMS.Utility {
    public abstract class BaseSqlWorker : ISqlWorker {
        protected BaseSqlWorker(IDbConnection connection, IDbCommand cmd = null) {
            Cmd = cmd ?? new SqlCommand();
            Cmd.Connection = connection;
            ConnectionString = connection.ConnectionString;
        }

        protected string ConnectionString { get; private set; }

        protected string FormatForSql(string parameterName) {
            return parameterName.Substring(0, 1) == "@" ? parameterName : string.Format("@{0}", parameterName);
        }

        public IDbCommand Cmd { get; set; }

        public abstract void SetupCommand(string SQL, CommandType commandType = CommandType.StoredProcedure);

        public abstract void AddInputParameter(string parameterName, object parameterValue, SqlCollectionType collectionType = SqlCollectionType.None, DbType type = DbType.AnsiString);

        public abstract void AddOutputParameter(string parameterName);

        public abstract T ReturnOutputParameter<T>();

        public virtual IEnumerable<T> GetObject<T>() {
            List<T> objectList = new List<T>();
            IEnumerable<DataTransferObjectMetaData> dtoMetadata = GetDtoMetaData<T>(); // Reflection is expensive. Only Get the Needed Column Names and Property Info Once!

            using (IDataReader rdr = GetDataReader()) {
                while (rdr.Read()) {
                    T instance = Activator.CreateInstance<T>();
                    foreach (var metadata in dtoMetadata) {
                        var fieldFromDb = rdr[metadata.DatabaseColumnName];
                        metadata.ReflectionPropertyInfo.SetValue(instance, fieldFromDb == DBNull.Value ? null : fieldFromDb, null);
                    }
                    objectList.Add(instance);
                } 
            }
            return objectList;
        }

        public abstract IDataReader GetDataReader();

        public abstract int ExecuteNonQuery();

        public abstract object ExecuteScalar();

        protected void OpenConnection() {
            if (Cmd.Connection.State == ConnectionState.Open)
                return;
            Cmd.Connection.Open();
        }

        protected void CloseConnection() {
            if (Cmd.Connection.State == ConnectionState.Closed)
                return;
            Cmd.Connection.Close();
        }
        protected IEnumerable<DataTransferObjectMetaData> GetDtoMetaData<T>() {
            Type type = typeof(T);
            IList<DataTransferObjectMetaData> metaData = new List<DataTransferObjectMetaData>();

            PropertyInfo[] propertyData = type.GetProperties();

            foreach (var itemData in propertyData) {
                DatabaseColumnNameAttribute attr = itemData.GetAttribute<DatabaseColumnNameAttribute>();
                string databaseColumn = attr == null ? string.Empty : attr.columnName;
                if (!itemData.HasAttribute(typeof(NotInQueryAttribute)))
                    metaData.Add(
                        new DataTransferObjectMetaData{ 
                            PropertyName = itemData.Name, 
                            DatabaseColumnName = string.IsNullOrEmpty(databaseColumn) ? itemData.Name : databaseColumn, 
                            ReflectionPropertyInfo = itemData
                    });
            }
            return metaData;
        }

        protected class DataTransferObjectMetaData {
            public string PropertyName { get; set; }

            public string DatabaseColumnName { get; set; }

            public PropertyInfo ReflectionPropertyInfo { get; set; }
        }
    }
}