using System;

namespace RealRMS.Utility.Attributes {
    public class DatabaseColumnNameAttribute : Attribute {
        public string columnName;
        public DatabaseColumnNameAttribute(string databaseColumnName) {
            columnName = databaseColumnName;
        }
    }

    public class NotInQueryAttribute : Attribute {
        
    }
}