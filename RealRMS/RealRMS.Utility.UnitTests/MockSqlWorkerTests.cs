using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RealRMS.Utility.UnitTests {
    [TestClass]
    public class MockSqlWorkerTests {
        protected MockSqlWorker worker;
        private Dictionary<string, object> expectedOutputParams;
        [TestInitialize]
        public void Init() {
            worker = new MockSqlWorker(new SqlConnection());
            expectedOutputParams = new Dictionary<string, object>();
        }

        #region Expected Query Without Parameters

        [TestMethod]
        public void TestMockWorker_WhenOneExpectedResultInitializedWithout_Params_ReturnExpectedRecordSet() {
            const string COMMAND = "sproc";
            string[] array = {"Blue", "Red", "Green", "Purple"};
            worker.Query(COMMAND).Produces(array);
            worker.SetupCommand(COMMAND);
            IDataReader rdr = worker.GetDataReader();
            while (rdr.Read()) {
                Assert.IsTrue(((IList)array).Contains(rdr[0]));
            }
        }

        [TestMethod]
        public void TestMockWorker_WhenMultipleExpectedResultsInitializedWithoutParameters_ReturnTheCorrectRecordSet() {
            const string COMMAND1 = "sproc";
            const string COMMAND2 = "sproc2";

            string[] array = { "Blue", "Red", "Green", "Purple" };
            string[] array2 = {"Gold", "Green", "Yellow", "Blue"};
            worker.Query(COMMAND1).Produces(array);
            worker.Query(COMMAND2).Produces(array2);

            worker.SetupCommand(COMMAND2);
            IDataReader rdr = worker.GetDataReader();
            while (rdr.Read()) {
                Assert.IsTrue(((IList)array2).Contains(rdr[0]));
            }            
        }

        [TestMethod]
        public void TestMockWorker_WhenNExpectedResultsInitializedWithoutParameters_ReturnTheCorrectOutput() {
            const string COMMAND1 = "sproc";
            const string COMMAND2 = "sproc2";
            const string COMMAND3 = "sproc3";

            string[] array = { "Blue", "Red", "Green", "Purple" };
            string[] array2 = { "Gold", "Green", "Yellow", "Blue" };
            string[] array3 = {"One", "Two", "Three"};

            worker.Query(COMMAND1).Produces(array);
            worker.Query(COMMAND2).Produces(array2);
            worker.Query(COMMAND3).Produces(array3);

            worker.SetupCommand(COMMAND1);
            IDataReader rdr = worker.GetDataReader();
            while (rdr.Read()) {
                Assert.IsTrue(((IList)array).Contains(rdr[0]));
            }                  
        }

        public void TestMockWorker_WhenExpectedResultsInitializedWithoutParameters_ReturnCorrectRecordCount() {
            const string COMMAND1 = "sproc";
            const string COMMAND2 = "sproc2";
            const string COMMAND3 = "sproc3";

            string[] array = { "Blue", "Red", "Green", "Purple" };
            string[] array2 = { "Gold", "Green", "Yellow", "Blue" };
            string[] array3 = { "One", "Two", "Three" };

            worker.Query(COMMAND1).Produces(array);
            worker.Query(COMMAND2).Produces(array2);
            worker.Query(COMMAND3).Produces(array3);

            worker.SetupCommand(COMMAND1);
            IDataReader rdr = worker.GetDataReader();
            while (rdr.Read())
            {
                Assert.IsTrue(((IList)array).Contains(rdr[0]));
            }             
        }

        #endregion

        #region Output Parameter Verification

        [TestMethod]

        public void TestAddOuputParameter_ReturnCorrectValue_ExecuteNonQuery() {
            const string PARAM_NAME = "@hello";
            const string PARAM_VALUE = "world";
            const string SPROC = "sproc";

            expectedOutputParams.Add(PARAM_NAME, PARAM_VALUE);
            worker.Query(SPROC).CalledWithOutput(PARAM_NAME).Produces(4, expectedOutputParams);
            worker.SetupCommand(SPROC);
            worker.AddOutputParameter(PARAM_NAME);

            Assert.AreEqual(PARAM_VALUE, worker.ReturnOutputParameter<string>());
        }

        [TestMethod]
        public void TestAddOutputParameter_ReturnCorrectValue_WhenReturningRecords() {
            const string PARAM_NAME = "@hello";
            const string PARAM_VALUE = "world";

            expectedOutputParams.Add(PARAM_NAME, PARAM_VALUE);
            worker.Query("sproc").CalledWithOutput(PARAM_NAME).Produces(4, expectedOutputParams);

            worker.SetupCommand("sproc");
            worker.AddOutputParameter(PARAM_NAME);
            Assert.AreEqual(PARAM_VALUE, worker.ReturnOutputParameter<string>());
        }

        #endregion

        #region Expected Query Matching

        [TestMethod]
        public void TestMock_WhenTwoExpectedCallsOfSameQueryWithDifferentParameters_ReturnTheCorrectOne() {
            const string COMMAND = "Bob";
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add("@one", 1);
            paramList.Add("@two", 2);

            Dictionary<string, object> paramList2 = new Dictionary<string,object>();
            paramList2.Add("@one", 11);
            paramList2.Add("@two", 22);

            IList<GenericModel> output = new List<GenericModel>();
            output.Add(new GenericModel {id = "1", name = "bob"});
            output.Add(new GenericModel {id = "2", name = "roger"});
            IList<GenericModel> output2 = new List<GenericModel>();
            output2.Add(new GenericModel {id = "40", name = "joe"});

            worker.Query(COMMAND).CalledWithInput(paramList).Produces(output);
            worker.Query(COMMAND).CalledWithInput(paramList2).Produces(output2);

            worker.SetupCommand(COMMAND);
            worker.AddInputParameter("@one", 11);
            worker.AddInputParameter("@two", 22);

            IDataReader rdr = worker.GetDataReader();
            using (rdr) {
                while (rdr.Read()) {
                    Assert.AreEqual("40", rdr["id"]);
                }
            }
        }

        private class GenericModel {
            public string id { get; set; }
            public string name { get; set; }
        }
        #endregion
    }
}
