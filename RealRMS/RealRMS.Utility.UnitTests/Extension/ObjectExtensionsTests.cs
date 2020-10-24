using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealRMS.Utility.Extension;

namespace RealRMS.Utility.UnitTests.Extension
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void TestSafeToString_whenNullObjectPassedIn_ReturnNull(){
            object o = null;
            Assert.IsNull(o.SafeToString());
        }

        [TestMethod]
        public void TestSafeToString_whenDBNullPassedIn_ReturnNull(){
            object o = DBNull.Value;
            Assert.IsNull(o.SafeToString());
        }

        [TestMethod]
        public void TestSafeToString_whenNumberPassedIn_ReturnStringifiedNumber() {
            double d = 4.37;
            Assert.AreEqual("4.37", d.SafeToString());
        }

        [TestMethod]
        public void TestSafeToString_whenStringPassedIn_ReturnThatString() {
            const string BLUE = "blue";
            object o = BLUE;
            Assert.AreEqual(BLUE, o.SafeToString());
        }
    }
}
