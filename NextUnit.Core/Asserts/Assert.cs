using System.Collections;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using NextUnit.Core.Extensions;

namespace NextUnit.Core.Asserts
{
    /// <summary>
    /// Assert class
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool AutoAssertMessages { get; set; } = false;

        /// <summary>
        /// This will be determining if the Asserts throw exceptions.
        /// Not yet fully implemented.
        /// 
        /// </summary>
        public static bool ThrowsExceptions { get; } = true;

        /// <summary>
        /// Method to compare two objects' properties for equality
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void CompareProperties(object a, object b, string message = "", int depth = 0)
        {
            ReflectionExtensions.CompareProperties(a, b, message, depth);
        }

        #region Comparison Assertions
        /// <summary>
        /// Throws an exception if the given T is NOT greater than expected.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsGreaterThan<T>(T expected, T actual, string message = "") where T : IComparable<T>
        {
            if (actual.CompareTo(expected) <= 0)
            {
                throw new AssertException($"{message}: Expected <{actual}> to be greater than <{expected}>.");
            }
        }

        /// <summary>
        /// Throws an exception if the given T is NOT greater or less than expected.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsGreaterThanOrEqual<T>(T expected, T actual, string message = "") where T : IComparable<T>
        {
            if (actual.CompareTo(expected) < 0)
            {
                throw new AssertException($"{message}: Expected <{actual}> to be greater than or equal to <{expected}>.");
            }
        }

        /// <summary>
        /// Throws an exception if it is greater or equal than T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        public static void IsLessThan<T>(T expected, T actual, string message = "") where T : IComparable<T>
        {
            if (actual.CompareTo(expected) >= 0)
            {
                throw new AssertException($"{message}: Expected <{actual}> to be less than <{expected}>.");
            }
        }

        /// <summary>
        /// Throws an exception if greater than T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
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
        /// Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects"></param>
        public static void Only<T>(IEnumerable<T> objects, Func<T, bool> predicate)
        {
            objects.Single(predicate);
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
            if (condition)
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
        /// Compares if the elements AND the sequence of the elements is equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void SequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (!expected.SequenceEqual(actual))
            {
                // Construct a detailed message to help diagnose the problem
                var expectedString = string.Join(", ", expected);
                var actualString = string.Join(", ", actual);
                var message = $"Expected sequence [{expectedString}], but found [{actualString}].";

                throw new InvalidOperationException(message); // Or use a more appropriate exception type or assertion failure specific to your testing framework
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expectedOrder"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void IsOrdered<T>(IEnumerable<T> expectedOrder)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            IComparer<T> comparer = Comparer<T>.Default;
            T lastItem = default(T);
            bool isFirstItem = true;

            foreach (var item in expectedOrder)
            {
                if (!isFirstItem && comparer.Compare(lastItem, item) > 0)
                {
                    throw new AssertException("List is not ordered."); // Or use your testing framework's assertion failure.
                }
                isFirstItem = false;
                lastItem = item;
            }
        }

        public static void IsSubsetOf<T>(IEnumerable<T> fullList, IEnumerable<T> subset)
        {
            if (!subset.All(element => fullList.Contains(element)))
            {
                throw new AssertException("The second collection is not a subset of the first.");
                // Replace InvalidOperationException with your testing framework's assertion failure.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullList"></param>
        /// <param name="subset"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void IsNotSubsetOf<T>(IEnumerable<T> fullList, IEnumerable<T> subset)
        {
            if (subset.All(element => fullList.Contains(element)))
            {
                throw new AssertException("The second collection is a subset of the first.");
                // Replace InvalidOperationException with your testing framework's assertion failure.
            }
        }

        /// <summary>
        /// This checks for both if they contain the same elements. Order does matter.
        /// </summary>
        public static void EqualElementsOrdered<T>(IEnumerable<T> enumerables1, IEnumerable<T> enumerables2)
        {
            EqualElements(enumerables1, enumerables2, considerOrder: true);
        }

        /// <summary>
        //// This checks for both if they contain the same elements. Order does not matter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerables1"></param>
        /// <param name="enumerables2"></param>
        public static void EqualElementsUnordered<T>(IEnumerable<T> enumerables1, IEnumerable<T> enumerables2)
        {
            EqualElements(enumerables1, enumerables2, considerOrder: false);
        }

        /// <summary>
        /// Function that's used by EqualElements implementation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerables1"></param>
        /// <param name="enumerables2"></param>
        /// <param name="considerOrder"></param>
        /// <exception cref="AssertException"></exception>
        private static void EqualElements<T>(IEnumerable<T> enumerables1, IEnumerable<T> enumerables2, bool considerOrder)
        {
            bool areEqual;

            if (considerOrder)
            {
                // Check if both sequences are equal considering the order of elements
                areEqual = enumerables1.SequenceEqual(enumerables2);
            }
            else
            {
                // Check if both sequences contain the same elements, disregarding the order
                var set1 = new HashSet<T>(enumerables1);
                var set2 = new HashSet<T>(enumerables2);
                areEqual = set1.SetEquals(set2);
            }

            if (!areEqual)
            {
                throw new AssertException("Collections do not match the specified equality condition.");
            }
        }


        /// <summary>
        /// Checks if two lists contain different elements, e.g:
        /// won't throw an exception if the lists would be for example:
        /// 
        /// a: a, b, c
        /// b: a, b, d
        /// 
        /// but will throw an exception if elements are all the same AND in the same sequence.
        /// </summary>
        /// <param name="enumerables1"></param>
        /// <param name="enumerables2"></param>
        public static void NotEqualElements<T>(IEnumerable<T> enumerables1, IEnumerable<T> enumerables2)
        {
            IEnumerable<(T first, T second)> zippedList = enumerables1.Zip(enumerables2);
            foreach (var enumerable in zippedList)
            {
                Assert.AreNotEqual(enumerable.first, enumerable.second);
            }
        }

        /// <summary>
        /// Checks if a number is in this range.
        /// </summary>
        public static void IsInRange<T>(T number, T min, T max) where T : IComparable<T>
        {
            if (number.CompareTo(min) < 0 || number.CompareTo(max) > 0)
            {
                throw new AssertException($"Number {number} is outside of range [{min},{max}].");
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
        /// Throws an exception if they are not the Same<typeparamref name="T"/>
        /// </summary>
        public static void Same<T>(T expected, T actual, string message)
        {
            if (!AreSame(expected, actual))
            {
                throw new AssertException($"{message}");
            }
        }

        /// <summary>
        /// Throws an exception if they are the Same<typeparamref name="T"/>
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
        /// Throws an exception if  IsOfType<typeparamref name="T"/>
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
        /// Checks if this type has the given property, if it is not found, we get an exception.
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
        /// Checks for an expected connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void ExpectedException<T>()
        {
            ExpectedException(typeof(T));
        }

        /// <summary>
        /// Makes the test pass, no matter what had happened before.
        /// </summary>
        public static void Pass(string message = null)
        {
            //this shouldn't throw an exception itself so far.
        }

        /// <summary>
        /// Makes the test fail, no matter what had happened before.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public static void Fail(string message = null)
        {
            throw new AssertException(message);
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

        #region Exception Tracking
        public delegate void AssertionDelegateExpectedActual<T>(T expected, T actual, string message = null);
        public delegate void AssertionDelegateExpectedActual(object expected, object actual, string message = null);

        public delegate void AssertionDelegateExpected<T>(T expected, string message = null);
        public delegate void AssertionDelegateExpected(object expected, string message = null);

        public static (bool result, Exception thrownException) Exception<T>(AssertionDelegateExpectedActual<T> assertionDelegate, T expected, T actual, string message)
        {
            //Creates a stack overflow still, because it calls the exactly same delegate.
            //throw new Exception($"Investigate overflow for {new StackFrame().GetMethod().Name}");
            return Exception(assertionDelegate, (object)expected, (object)actual, message);
        }

        public static (bool result, Exception thrownException) Exception<T>(Delegate assertionDelegate, T expected, IEnumerable<T> actual, string message = null)
        {
            bool exceptionWasThrown = false;
            Exception thrownException = null;
            try
            {
                // Since Delegate.DynamicInvoke expects object[], wrap arguments in an object array
                assertionDelegate.DynamicInvoke(new object[] { expected, actual, message });
            }
            catch (TargetInvocationException ex) when (ex.InnerException != null)
            {
                // TargetInvocationException wraps the actual exception thrown in the InnerException property
                thrownException = ex.InnerException;
                exceptionWasThrown = true;
            }
            catch (Exception ex)
            {
                thrownException = ex;
                exceptionWasThrown = true;
            }
            return (exceptionWasThrown, thrownException);
        }

        public static (bool result, Exception thrownException) Exception(AssertionDelegateExpectedActual assertionDelegate, object expected, object actual, string message = null)
        {
            bool exceptionWasThrown = false;
            Exception thrownException = null;
            try
            {
                assertionDelegate(expected, actual, message);
            }
            catch (Exception ex)
            {
                thrownException = ex;
                exceptionWasThrown = true;
            }
            return (exceptionWasThrown, thrownException);
        }

        public static (bool result, Exception thrownException) Exception<T>(AssertionDelegateExpected<T> assertionDelegateExpected, T expected, string message = null)
        {
            bool exceptionWasThrown = false;
            Exception thrownException = null;
            try
            {
                assertionDelegateExpected(expected, message);
            }
            catch (Exception ex)
            {
                thrownException = ex;
                exceptionWasThrown = true;
            }
            return (exceptionWasThrown, thrownException);
        }

        public static (bool result, Exception thrownException) Exception<T>(AssertionDelegateExpectedActual<T> assertionDelegate, params object[] values)
        {
            bool exceptionWasThrown = false;
            Exception thrownException = null;
            try
            {
                if (values != null && values.Length > 0 && values[0] != null && values[1] != null)
                {
                    assertionDelegate((T)values[0], (T)values[1], (values.Length > 2 && values[2] != null) ? (values[2] as string) : null);
                }
            }
            catch (Exception ex)
            {
                thrownException = ex;
                exceptionWasThrown = true;
            }
            return (exceptionWasThrown, thrownException);
        }

        /// <summary>
        /// Executes the specified action and returns any exception that was thrown.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>A tuple indicating whether an exception was thrown and the exception itself.</returns>
        public static (bool exceptionThrown, Exception exception) TryCatch(Action action)
        {
            try
            {
                action();
                return (false, null); // No exception was thrown
            }
            catch (Exception ex)
            {
                return (true, ex); // An exception was thrown
            }
        }

        public static (bool result, Exception thrownException) Exception(Delegate assertionDelegate, params object[] args)
        {
            bool exceptionWasThrown = false;
            Exception thrownException = null;

            try
            {
                // Dynamically invoke the delegate with the provided arguments.
                assertionDelegate.DynamicInvoke(args);
            }
            catch (Exception ex)
            {
                // Catch the base Exception to simplify the handling logic.
                // Consider catching more specific exceptions if needed.
                thrownException = ex;
                exceptionWasThrown = true;
            }

            return (exceptionWasThrown, thrownException);
        }
        #endregion Exception Tracking

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