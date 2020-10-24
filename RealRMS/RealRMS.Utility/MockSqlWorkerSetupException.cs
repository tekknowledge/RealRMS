using System;

namespace RealRMS.Utility {
    public class MockSqlWorkerSetupException : InvalidOperationException {
        public MockSqlWorkerSetupException(string message) : base(message) { }
    }
}