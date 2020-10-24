using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using RealRMS.Utility.Extension;

namespace RealRMS.Utility {
    public class MockSqlWorker : BaseSqlWorker {

        #region Constructor

        public MockSqlWorker(IDbConnection connection) : base(connection) {
            expectedResults = new List<ExpectedResult>();
        }

        #endregion

        #region Fake Data Retrieval

        private class ExpectedResult {
            public ExpectedResult() {
                Parameters = new Dictionary<string, object>();
                ParameterNames = new List<string>();
                ParameterValues = new List<object>();
                OutputParameters = new Dictionary<string, object>();
            }
            public IDictionary<string, object> Parameters { get; set; }

            public IList<string> ParameterNames { get; set; }

            public IList<object> ParameterValues { get; set; }

            public IDataReader ResultSet { get; set; }

            public int RecordCount { get; set; }

            public string SqlStatement { get; set; }

            public IDictionary<string, object> OutputParameters { get; set; }
        }

        #endregion

        #region Fake Query Setup 

        private IList<ExpectedResult> expectedResults;
        private ExpectedResult expectedResult;

        #region Query Expectation 

        public MockSqlWorker Query(string sql) {
            expectedResult = new ExpectedResult();
            expectedResult.SqlStatement = sql;
            return this;
        }

        public MockSqlWorker CalledWithInput(Dictionary<string, object> input) {
            CheckForQuery();
            foreach (KeyValuePair<string, object> pair in input) {
                expectedResult.ParameterNames.Add(pair.Key);
                expectedResult.ParameterValues.Add(pair.Value);
            }
            expectedResult.Parameters = input;
            return this;
        }

        public MockSqlWorker CalledWithOutput(string output) {
            string[] arr = {output};
            return CalledWithOutput(arr.ToList());
        }

        public MockSqlWorker CalledWithOutput(List<string> output) {
            CheckForQuery();
            foreach (var item in output) {
                expectedResult.OutputParameters.Add(item, null);
            }
            return this;
        }

        #endregion

        #region QueryOutput

        public void Produces<T>(IEnumerable<T> data, IDictionary<string, object> outputParams = null) {
            CheckForQuery();
            SetOutputParams(outputParams);
            expectedResult.ResultSet = CreateDataTable<T>(data).CreateDataReader();
            expectedResults.Add(expectedResult);

            expectedResult = null;
        }

        public void Produces(int recordCount, IDictionary<string, object> outputParams = null) {
            CheckForQuery();
            SetOutputParams(outputParams);
            expectedResult.RecordCount = recordCount;
            expectedResults.Add(expectedResult);
            expectedResult = null;
        }

        #endregion

        #endregion

        #region Properties

        string currentCommand { get; set; }
        
        IDictionary<string, object> currentInputParameters { get; set; }

        List<string> currentOutputParameters { get; set; }

        #endregion

        #region Implementation of ISqlWorker

        public override void SetupCommand(string SQL, CommandType commandType = CommandType.StoredProcedure) {
            currentCommand = SQL;
            currentInputParameters = new Dictionary<string, object>();
            currentOutputParameters = new List<string>();
        }

        public override void AddInputParameter(string parameterName, object parameterValue,  SqlCollectionType collectionType = SqlCollectionType.None, DbType type = DbType.AnsiString) {
            currentInputParameters.Add(parameterName, parameterValue);

        }

        public override void AddOutputParameter(string parameterName) {
            currentOutputParameters.Add(parameterName);
        }

        public override T ReturnOutputParameter<T>() {
            SetExpectedResult();
            return (T)expectedResult.OutputParameters.First().Value;
        }

        public override IDataReader GetDataReader() {
            SetExpectedResult();
            return expectedResult.ResultSet;
        }

        public override int ExecuteNonQuery() {
            SetExpectedResult();
            return expectedResult.RecordCount;
        }

        public override object ExecuteScalar() {
            SetExpectedResult();
            if (expectedResult.ResultSet.Read()) {
                return expectedResult.ResultSet[0];
            }
            return null;
        }

        #endregion

        #region Helper Methods

        private void SetOutputParams(IDictionary<string, object> paramsToSet) {
            if (paramsToSet != null) {
                foreach (KeyValuePair<string, object> param in paramsToSet) {
                    expectedResult.OutputParameters[param.Key] = param.Value;
                }
            }           
        }

        private void CheckForQuery() {
            const string ERROR_MESSAGE = "The query method must be called before setting up expected input and output parameters.";
            if (expectedResult == null || string.IsNullOrEmpty(expectedResult.SqlStatement))
                throw new MockSqlWorkerSetupException(ERROR_MESSAGE);
        }

        private void SetExpectedResult() {
            IEnumerable<ExpectedResult> results = expectedResults.Where(r => r.SqlStatement.EqualsCaseInsensitive(currentCommand));
            if (currentInputParameters.Count > 0)
                results = results.Where(r => Match(r.ParameterNames, r.ParameterValues, currentInputParameters));
            if (currentOutputParameters.Count > 0)
                results = results.Where(r => r.OutputParameters.Keys.IsEqualTo(currentOutputParameters));
            expectedResult = results.FirstOrDefault();
        }

        private bool Match(IList<string> requiredPassedInParameterNames, IList<object> parameterValueList, IDictionary<string, object> passedInParameters) {
            int index = 0;
            Dictionary<int, string> indexOfRequiredParameterWithValue = new Dictionary<int, string>();

            // Match the name of the required parameters to a name of a passed in one. There can be extra actual parameters passed in, as long
            // as all parameter names are covered. If the parameter names match, output the index and value so we an get it from the other array.
            // This implementation (instead of twin dictionaries) was chosen to produce the most efficient matching operations I could think of.
            foreach (var requiredParameter in requiredPassedInParameterNames) {
                if (passedInParameters.ContainsKey(requiredParameter))
                    indexOfRequiredParameterWithValue.Add(index, passedInParameters[requiredParameter].SafeToString());
                else
                    return false;
                index++;
            }

            return indexOfRequiredParameterWithValue.All(pair => parameterValueList[pair.Key].SafeToString() == pair.Value);
        }

        private static DataTable CreateDataTable<T>(IEnumerable<T> list) {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            bool isPrimitiveType = type.IsValueType || (type == typeof(string)) || type.IsPrimitive;
            if (isPrimitiveType)
                dataTable = populatePrimitive(list);
            else
                dataTable = populateComplex(properties, list);

            return dataTable;
        }

        private static DataTable populatePrimitive<T>(IEnumerable<T> list) {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            foreach (T value in list) {
                dt.Rows.Add(value);
            }
            return dt;
        }

        private static DataTable populateComplex<T>(PropertyInfo[] properties, IEnumerable<T> list) {
            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties) {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list) {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++) {
                    values[i] = properties[i].GetValue(entity, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion
    }
}