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

        /// <summary>
        /// Assert IsNull
        /// </summary>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsNull(object objectToCheck, string message = null)
        {
            if (objectToCheck == null)
            {
                throw new AssertException($"{message}: The object is: {objectToCheck}");
            }
        }

        /// <summary>
        /// Assert IsNull<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        public static void IsNull<T>(object objectToCheck, string message = null)
        {
            IsNull(objectToCheck, message);
        }

        /// <summary>
        /// Assert IsNotNull
        /// </summary>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsNotNull(object objectToCheck, string message = null)
        {
            if (objectToCheck == null)
            {
                throw new AssertException($"{message}: The object is: {objectToCheck}");
            }
        }

        /// <summary>
        /// Assert IsNotNull<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        public static void IsNotNull<T>(object objectToCheck, string message = null)
        {
            IsNull(objectToCheck, message);
        }

        /// <summary>
        /// Assert IsOfType<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsOfType<T>(Type type, string message = null)
        {
            if (!(type is T))
            {
                throw new AssertException($"{message}");
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
        /// Assert Same<typeparamref name="T"/>
        /// </summary>
        public static void Same<T>(T expected, T actual)
        {
            if (!AreSame(expected, actual))
            {
                throw new AssertException("");
            }
        }

        
        /// <summary>
        /// Assert NotSame<typeparamref name="T"/>
        /// </summary>
        public static void NotSame<T>(T expected, T actual)
        {
            if (AreSame(expected, actual))
            {
                throw new AssertException("");
            }
        }

        /// <summary>
        /// Assert Pass
        /// </summary>
        public static void Pass()
        {
            throw new NotImplementedException();
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