using NextUnitTestRunner.Assertions;

namespace NextUnitTestRunner
{
    public static class Assert
    {
        public static void IsTrue(bool condition)
        {
            if (!condition)
            {
                throw new AssertException($"Should be true but was {condition}.");
            }
        }

        public static void IsFalse(bool condition)
        {
            if (!condition)
            {
                throw new AssertException($"Should be false but was {condition}.");
            }
        }

        public static void AreEqual<T>(T expected, T actual)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertException("");
            }
        }
        public static void AreEqual(object expected, object actual)
        {
            if (!expected.Equals(actual))
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
