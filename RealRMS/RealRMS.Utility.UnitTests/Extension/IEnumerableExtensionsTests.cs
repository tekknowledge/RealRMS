using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealRMS.Utility.Extension;

namespace RealRMS.Utility.UnitTests.Extension {
    [TestClass]
    public class IEnumerableExtensionsTests {
        [TestMethod]
        public void TestIsEqualsTo_WhereSetsSameInDifferentOrder_ReturnTrue() {
            string[] arr1 = {"2", "8", "17", "46", "4"};
            string[] arr2 = {"4", "46", "2", "17", "8"};
            Assert.IsTrue(arr1.IsEqualTo(arr2));

        }

        [TestMethod]
        public void TestIsEqualsTo_WhereSetsSameInOrder_ReturnTrue() {
            string[] arr1 = { "2", "8", "17", "46", "4" };
            string[] arr2 = { "2", "8", "17", "46", "4" };
            Assert.IsTrue(arr1.IsEqualTo(arr2));

        }

        [TestMethod]
        public void TestIsEqualsTo_WhereSetsSameEmpty_ReturnTrue() {
            List<string> arr1 = new List<string>();
            List<string> arr2 = new List<string>();
            Assert.IsTrue(arr1.IsEqualTo(arr2));

        }
        [TestMethod]
        public void TestIsEqualsTo_WhereSetsDifferentLength_ReturnFalse() {
            string[] arr1 = { "2", "8", "17", "46", "4" };
            string[] arr2 = { "4", "46", "2", "17"};
            Assert.IsFalse(arr1.IsEqualTo(arr2));

        }

        [TestMethod]
        public void TestIsEqualsTo_WhereSetsSameLengthWithDifferentNumbers_ReturnFalse() {
            string[] arr1 = { "2", "8", "17", "46", "4" };
            string[] arr2 = { "4", "46", "2", "17", "40" };
            Assert.IsFalse(arr1.IsEqualTo(arr2));

        }

        [TestMethod]
        public void TestIsEqualsTo_WhereOneSetPopulatedOneEmpty_ReturnFalse() {
            string[] arr1 = { "2", "8", "17", "46", "4" };
            List<string> arr2 = new List<string>();
            Assert.IsFalse(arr1.IsEqualTo(arr2));
        }
    }
}
