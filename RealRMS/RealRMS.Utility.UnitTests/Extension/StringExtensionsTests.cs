using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealRMS.Utility.Extension;

namespace RealRMS.Utility.UnitTests.Extension
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void TestEqualsCaseInsensitive_WhenBothValuesNull_ReturnTrue() {
            string test1 = null;
            string test2 = null;
            Assert.IsTrue(test1.EqualsCaseInsensitive(test2));
        }

        [TestMethod]
        public void TestEqualsCaseInsensitive_WhenTargetStringNullAndComparisonNot_ReturnFalse() {
            string test1 = null;
            string test2 = "null";
            Assert.IsFalse(test1.EqualsCaseInsensitive(test2));
        }

        [TestMethod]
        public void TestEqualsCaseInsensitive_WhenComparisonStringNullAndTargetNot_ReturnFalse() {
            string test1 = "null";
            string test2 = null;
            Assert.IsFalse(test1.EqualsCaseInsensitive(test2));
        }

        [TestMethod]
        public void TestEqualsCaseInsensitive_BothStringsEmpty_ReturnTrue() {
            string test1 = string.Empty;
            string test2 = string.Empty;
            Assert.IsTrue(test1.EqualsCaseInsensitive(test2));
        }

        [TestMethod]
        public void TestEqualsCaseInsensitive_BothStringsEqual_ReturnTrue() {
            string test1 = "Medium";
            string test2 = "Medium";
            Assert.IsTrue(test1.EqualsCaseInsensitive(test2));
        }

        [TestMethod]
        public void TestEqualsCaseInsensitive_BothStringsEqualBesidesCasing_ReturnTrue() {
            string test1 = "JoSePh";
            string test2 = "jOsEpH";
            Assert.IsTrue(test1.EqualsCaseInsensitive(test2));
        }
    }
}
