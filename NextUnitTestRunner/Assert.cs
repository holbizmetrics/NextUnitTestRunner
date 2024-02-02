using NextUnit.TestRunner.Assertions;

namespace NextUnit.TestRunner
{
    /// <summary>
    /// 
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
            {
                throw new AssertException($"{message} Should be true but was {condition}.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsFalse(bool condition, string message = null)
        {
            if (!condition)
            {
                throw new AssertException($"{message} Should be false but was {condition}.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertException($"{message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertException("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToCheck"></param>
        /// <exception cref="AssertException"></exception>
        public static void NotNull(object objectToCheck)
        { 
            if (objectToCheck == null)
            {
                throw new AssertException("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExceptionType"></param>
        public static void ExpectedException(Type ExceptionType)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToTestAgainstNull"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsNotNull(object objectToTestAgainstNull)
        {
            if (objectToTestAgainstNull == null)
            {
                throw new AssertException($"{objectToTestAgainstNull} is null.");
            }
        }
    }
}
