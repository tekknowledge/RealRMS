using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealRMS.Utility.Attributes;
using RealRMS.Utility.Extension;

namespace RealRMS.Utility.UnitTests
{
    [TestClass]
    public class BaseSqlWorkerTests
    {
        private class TestSqlWorker : BaseSqlWorker {
            public TestSqlWorker(IDbConnection conn) : base(conn) { }

            public override void SetupCommand(string SQL, CommandType commandType = CommandType.StoredProcedure) {
                throw new NotImplementedException();
            }

            public override void AddInputParameter(string parameterName, object parameterValue, SqlCollectionType collectionType = SqlCollectionType.None, DbType dbType = DbType.AnsiString) {
                throw new NotImplementedException();
            }

            public override void AddOutputParameter(string parameterName) {
                throw new NotImplementedException();
            }

            public override T ReturnOutputParameter<T>() {
                throw new NotImplementedException();
            }

            public override IDataReader GetDataReader() {
                throw new NotImplementedException();
            }

            public override int ExecuteNonQuery() {
                throw new NotImplementedException();
            }

            public override object ExecuteScalar() {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void TestFormatForSql_WhenParameterAddedWithoutSymbol_AppendSymbol() {
            const string PARAM_NAME = "ralph";

            TestSqlWorker tester = new TestSqlWorker(new SqlConnection());
            PrivateObject test = new PrivateObject(tester);
            string result = test.Invoke("FormatForSql", PARAM_NAME).SafeToString();
            Assert.AreEqual(string.Format("@{0}", PARAM_NAME), result);
        }

        [TestMethod]
        public void TestFormatForSql_WhenParameterAddedWithSymbol_DoNotAppendSymbol() {
            const string PARAM_NAME = "@ralph";

            TestSqlWorker tester = new TestSqlWorker(new SqlConnection());
            PrivateObject test = new PrivateObject(tester);
            string result = test.Invoke("FormatForSql", PARAM_NAME).SafeToString();
            Assert.AreEqual(PARAM_NAME, result);
        }

        [TestMethod]
        public void TestGetObject() {
            MockSqlWorker worker = new MockSqlWorker(new SqlConnection());
            const string QUERY = "Query";

            MyFakeObject obj1 = new MyFakeObject() {
                id = 1,
                name = "one",
                displayName = "1-one",
                truth = true,
                doubles = 1.1
            };
            MyFakeObject[] objs = {obj1};
            worker.Query(QUERY).Produces(objs);
            
            worker.SetupCommand(QUERY);
            IEnumerable<MyFakeObject> objects = worker.GetObject<MyFakeObject>();
            var tester = objects.First();
            Assert.AreEqual(obj1.id, tester.id);
            Assert.AreEqual(obj1.name, tester.name);
            Assert.AreEqual(obj1.displayName, tester.displayName);
            Assert.AreEqual(obj1.truth, tester.truth);
            Assert.AreEqual(obj1.doubles, tester.dbl);
            Assert.AreEqual(0, tester.doubles);
        }

        private class MyFakeObject {
            public int id { get; set; }

            public string name { get; set; }

            public string displayName { get; set; }

            public bool truth { get; set; }

            [DatabaseColumnName("doubles")]
            public double dbl { get; set; }

            [NotInQuery]
            public double doubles { get; set; }

        }
    }
}
