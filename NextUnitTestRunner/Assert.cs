using NextUnitTestRunner.Assertions;

namespace NextUnitTestRunner
{
    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
            {
                throw new AssertException($"{message} Should be true but was {condition}.");
            }
        }

        public static void IsFalse(bool condition, string message = null)
        {
            if (!condition)
            {
                throw new AssertException($"{message} Should be false but was {condition}.");
            }
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertException($"{message}");
            }
        }
        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertException("");
            }
        }

        public static void NotNull(object objectToCheck)
        { 
            if (objectToCheck == null)
            {
                throw new AssertException("");
            }
        }

        public static void ExpectedException(Type ExceptionType)
        {

        }

        public static void IsNotNull(object objectToTestAgainstNull)
        {
            if (objectToTestAgainstNull == null)
            {
                throw new AssertException($"{objectToTestAgainstNull} is null.");
            }
        }
    }
}
