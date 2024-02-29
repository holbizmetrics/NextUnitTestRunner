using NextUnit.Core.TestAttributes;
using System.Diagnostics;

namespace NextUnit.Core.Tests.Attributes
{
    public class ConditionalRetryAttributeTests
    {
        private static int _externalServiceState = 0;
        private static bool IsServiceInDesiredState()
        {
            return _externalServiceState == 5;
        }

        [Test]
        [Group(nameof(ConditionalRetryAttribute))]
        [ConditionalRetry(nameof(IsServiceInDesiredState), maxRetry: 10)]
        public void TestExternalServiceInteraction()
        {
            _externalServiceState++;
            Trace.WriteLine($"Attempt {_externalServiceState}: Testing interaction with the external service");
        }

        [Test]
        [Group(nameof(ConditionalRetryAttribute))]
        [ConditionalRetry(nameof(IsServiceInDesiredState), 1)]
        public void ConditionalRetryAttributeTest()
        {

        }
    }
}
