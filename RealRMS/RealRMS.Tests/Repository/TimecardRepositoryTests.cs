using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealRMS.Models;
using RealRMS.Repository;
using RealRMS.Utility;

namespace RealRMS.Tests.Repository
{
    
    [TestClass]
    public class TimecardRepositoryTests {

        private class TimecardData {
            public int Emp_Id { get; set; }
            public int Id { get; set; }
            
            public DateTime? In { get; set; }
            
            public DateTime? Out { get; set; }
        }

        protected MockSqlWorker broker;
        protected FakeConnectionFactory connection;
        protected TimeCardRepository repo;
        protected Dictionary<string, object> paramSet1 = new Dictionary<string, object>();
        protected FakeCacheWrapper cache = new FakeCacheWrapper();

        [TestInitialize]
        public void init() {
            broker = new MockSqlWorker(new SqlConnection());
            connection = new FakeConnectionFactory();
            repo = new TimeCardRepository(cache, connection, broker);
        }
        [TestMethod]
        public void TestGet_WhenUserHasRecordWithOnlyStartTimeInLast12Hours_ReturnHydratedObject() {
            paramSet1.Add("@EmployeeId", 1);
            List<TimecardData> resultSet = new List<TimecardData>();
            TimecardData data = new TimecardData();
            data.Emp_Id = 1;
            data.Id = 1;
            data.In = DateTime.Now.Subtract(new TimeSpan(4, 0, 0));
            data.Out = null;
            resultSet.Add(data);
            broker.Query(TimeCardRepository.TIMECARD_GETFOR_EMPLOYEE).CalledWithInput(paramSet1).Produces(resultSet);
            TimeCardModel model = repo.Get(1);
            Assert.AreEqual(1, model.EmployeeId );
            Assert.IsNotNull(model.In);
            Assert.IsNull(model.Out);
        }

        [TestMethod]
        public void TestGet_WhenUserHasRecordWithOnlyStartTime12HoursAgo_ReturnNewObject()
        {
            paramSet1.Add("@EmployeeId", 1);
            List<TimecardData> resultSet = new List<TimecardData>();
            TimecardData data = new TimecardData();
            data.Emp_Id = 1;
            data.Id = 1;
            data.In = DateTime.Now.Subtract(new TimeSpan(13, 0, 0));
            data.Out = null;
            resultSet.Add(data);
            broker.Query(TimeCardRepository.TIMECARD_GETFOR_EMPLOYEE).CalledWithInput(paramSet1).Produces(resultSet);
            TimeCardModel model = repo.Get(1);
            Assert.AreEqual(1, model.EmployeeId);
            Assert.IsNull(model.In);
            Assert.IsNull(model.Out);
        }

        [TestMethod]
        public void TestGet_WhenUserHasNoRecord_ReturnNewObject()
        {
            paramSet1.Add("@EmployeeId", 1);
            List<object> resultSet = new List<object>();
            broker.Query(TimeCardRepository.TIMECARD_GETFOR_EMPLOYEE).CalledWithInput(paramSet1).Produces(resultSet);
            TimeCardModel model = repo.Get(1);
            Assert.AreEqual(1, model.EmployeeId);
            Assert.IsNull(model.In);
            Assert.IsNull(model.Out);
        }

        [TestMethod]
        public void TestGet_WhenUserHasFilledRecord_ReturnNewObject()
        {
            paramSet1.Add("@EmployeeId", 1);
            List<TimecardData> resultSet = new List<TimecardData>();
            TimecardData data = new TimecardData();
            data.Emp_Id = 1;
            data.Id = 1;
            data.In = DateTime.Now.Subtract(new TimeSpan(4, 0, 0));
            data.Out = DateTime.Now.Subtract(new TimeSpan(3, 0, 0));
            resultSet.Add(data);
            broker.Query(TimeCardRepository.TIMECARD_GETFOR_EMPLOYEE).CalledWithInput(paramSet1).Produces(resultSet);
            TimeCardModel model = repo.Get(1);
            Assert.AreEqual(1, model.EmployeeId);
            Assert.IsNull(model.In);
            Assert.IsNull(model.Out);
        }
    }
}
