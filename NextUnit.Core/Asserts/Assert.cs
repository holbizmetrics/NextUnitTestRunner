namespace NextUnit.Core.Asserts
{
    /// <summary>
    /// 
    /// </summary>
    public static class Assert
    {
        public static bool AutoAssertMessages { get; set; } = false;

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

        public static void IsNull(object objectToCheck, string message = null)
        {
            if (objectToCheck == null)
            {
                throw new AssertException($"{message}: The object is: {objectToCheck}");
            }
        }

        public static void IsNull<T>(object objectToCheck, string message = null)
        {
            IsNull(objectToCheck, message);
        }

        public static void IsNotNull(object objectToCheck, string message = null)
        {
            if (objectToCheck == null)
            {
                throw new AssertException($"{message}: The object is: {objectToCheck}");
            }
        }

        public static void IsNotNull<T>(object objectToCheck, string message = null)
        {
            IsNull(objectToCheck, message);
        }

        public static void IsOfType<T>(Type type, string message = null)
        {
            if (!(type is T))
            {
                throw new AssertException("");
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
                throw new AssertException($"{message}: Should be false but was {condition}.");
            }
        }

        public static void IsOfType<T>(object objectToCheckTypeOf)
        {

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void ExpectedException<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Same<T>(T expected, T actual)
        {
            if (!AreSame(expected, actual))
            {
                throw new AssertException("");
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        public static void NotSame<T>(T expected, T actual)
        {
            if (AreSame(expected, actual))
            {
                throw new AssertException("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Pass()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        private static bool AreSame(object obj1, object obj2)
        {
            return object.ReferenceEquals(obj1, obj2);
        }
    }
}