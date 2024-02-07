namespace NextUnit.Core.Asserts
{
    /// <summary>
    /// 
    /// </summary>
    public static class Assert
    {
        public static bool AutoAssertMessages { get; set; } = false;
        
        #region Comparison Assertions
        public static void IsGreaterThan<T>(T expected, T actual, string message = "") where T : IComparable<T>
        {
            if (actual.CompareTo(expected) <= 0)
            {
                throw new AssertException($"{message}: Expected <{actual}> to be greater than <{expected}>.");
            }
        }

        public static void IsGreaterThanOrEqual<T>(T expected, T actual, string message = "") where T : IComparable<T>
        {
            if (actual.CompareTo(expected) < 0)
            {
                throw new AssertException($"{message}: Expected <{actual}> to be greater than or equal to <{expected}>.");
            }
        }

        public static void IsLessThan<T>(T expected, T actual, string message = "") where T : IComparable<T>
        {
            if (actual.CompareTo(expected) >= 0)
            {
                throw new AssertException($"{message}: Expected <{actual}> to be less than <{expected}>.");
            }
        }

        public static void IsLessThanOrEqual<T>(T expected, T actual, string message = "") where T : IComparable<T>
        {
            if (actual.CompareTo(expected) > 0)
            {
                throw new AssertException($"{message}: Expected <{actual}> to be less than or equal to <{expected}>.");
            }
        }
        #endregion Comparison Assertions

        #region Collection Assertions
        /// <summary>
        /// Throws an exception if the collection is not empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsEmpty<T>(IEnumerable<T> collection, string message = "")
        {
            if (collection.Any())
            {
                throw new AssertException($"{message}: Expected collection to be empty.");
            }
        }

        /// <summary>
        /// Throws an exception if the collection is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsNotEmpty<T>(IEnumerable<T> collection, string message = "")
        {
            if (!collection.Any())
            {
                throw new AssertException($"{message}: Expected collection not to be empty.");
            }
        }

        /// <summary>
        /// Throws an exception if the collection does not contain the expected item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expectedItem"></param>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void Contains<T>(T expectedItem, IEnumerable<T> collection, string message = "")
        {
            if (!collection.Contains(expectedItem))
            {
                throw new AssertException($"{message}: Collection does not contain expected item.");
            }
        }

        /// <summary>
        /// Asserts if a collection does contain the unexpected item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unexpectedItem"></param>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void DoesNotContain<T>(T unexpectedItem, IEnumerable<T> collection, string message = "")
        {
            if (collection.Contains(unexpectedItem))
            {
                throw new AssertException($"{message}: Collection contains unexpected item.");
            }
        }
        #endregion Collection Assertions

        #region boolean/reference assertions (True, False, Same, Equals, etc.)
        /// <summary>
        /// Throws an exception if condition is false.
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
        /// Throws an exception if the object is not null.
        /// </summary>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsNull(object objectToCheck, string message = null)
        {
            if (objectToCheck != null)
            {
                throw new AssertException($"{message ?? "Expected object to be null, but it was not."}");
            }
        }

        /// <summary>
        /// Assert IsNull<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        public static void IsNull<T>(T objectToCheck, string message = null)
        {
            IsNull((object)objectToCheck, message);
        }

        /// <summary>
        /// Throws an exception if the object is null.
        /// </summary>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsNotNull(object objectToCheck, string message = null)
        {
            if (objectToCheck == null)
            {
                throw new AssertException($"{message ?? "Expected object to be not null, but it was."}");
            }
        }

        /// <summary>
        /// Assert IsNotNull<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        public static void IsNotNull<T>(T objectToCheck, string message = null)
        {
            IsNotNull((object)objectToCheck, message);
        }

        /// <summary>
        /// Throws an exception if the condition is true..
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
                AreEqual((object)expected, (object)actual, message);
            }
        }

        /// <summary>
        /// Throws an exception if the objects are not equal.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertException($"{message}");
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
        public static void AreNotEqual<T>(T expected, T actual, string message = null)
        {
            if (!expected.Equals(actual))
            {
                AreEqual((object)expected, (object)actual, message);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        public static void AreNotEqual(object expected, object actual, string message = null)
        {
            if (!expected.Equals(actual))
            {
                AreNotEqual((object)expected, (object)actual, message);
            }
        }

        /// <summary>
        /// Assert Same<typeparamref name="T"/>
        /// </summary>
        public static void Same<T>(T expected, T actual, string message)
        {
            if (!AreSame(expected, actual))
            {
                throw new AssertException($"{message}");
            }
        }

        /// <summary>
        /// Assert NotSame<typeparamref name="T"/>
        /// </summary>
        public static void NotSame<T>(T expected, T actual, string message)
        {
            if (AreSame(expected, actual))
            {
                throw new AssertException($"{message}");
            }
        }
        #endregion boolean/reference assertions (True, False, Same, Equals, etc.)

        #region Type/Property assertions
        /// <summary>
        /// Assert IsOfType<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsOfType<T>(object objectToCheck, string message = "")
        {
            if (!(objectToCheck is T))
            {
                throw new AssertException($"{message}: Object is not of type {typeof(T).Name}.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="objectToCheck"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void HasProperty(string propertyName, object objectToCheck, string message = "")
        {
            var property = objectToCheck.GetType().GetProperty(propertyName);
            if (property == null)
            {
                throw new AssertException($"{message}: Object does not have property {propertyName}.");
            }
        }
        #endregion Type/Property assertions

        #region Flow Controlling Assertions
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
            ExpectedException(typeof(T));
        }

        /// <summary>
        /// Makes the test pass, no matter what had happened before.
        /// </summary>
        public static void Pass()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Makes the test fail, no matter what had happened before.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public static void Fail()
        {
            throw new NotImplementedException();
        }
        #endregion Flow Controlling Assertions

        #region Exception Assertions
        public static void Throws<TException>(Action action, string message = "") where TException : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (TException)
            {
                return; // Test passes
            }
            catch (Exception ex)
            {
                throw new AssertException($"{message}: Expected exception of type {typeof(TException).Name} but caught {ex.GetType().Name}.");
            }
            throw new AssertException($"{message}: Expected exception of type {typeof(TException).Name} but no exception was thrown.");
        }

        public static void DoesNotThrow<TException>(Action action, string message = "") where TException : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (TException ex)
            {
                throw new AssertException($"{message}: Expected no exception of type {typeof(TException).Name} but caught one.");
            }
        }
        #endregion Exception Assertions

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