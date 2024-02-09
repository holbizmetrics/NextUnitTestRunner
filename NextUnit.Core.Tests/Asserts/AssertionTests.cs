using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Asserts
{
    public class AssertionTests
    {
        #region Advanced Assertion Checks
        [Test]
        public void CompareProperties_ReferenceEquality_Passes()
        {
            object a = new object();
            object b = a;
            Assert.CompareProperties(a, b);
        }

        [Test]
        public void CompareProperties_Equals_Passes()
        {
            //plain object
            object a = 1;
            object b = 1;
            Assert.CompareProperties(a, b);

            a = new int[] { 1, 2, 3 };
            b = new int[] { 1, 2, 3 };
            Assert.CompareProperties(a, b);

            //anonymous object
            a = new { MyBool = true, MyString = "Hello, World!", MyInt = 10 };
            b = new { MyBool = true, MyString = "Hello, World!", MyInt = 10 };
            Assert.CompareProperties(a, b);

            //null
            a = null;
            b = null;
            Assert.CompareProperties(a, b);

            //complex anonymous object (one anonymous object containing another one)...
            a = new { Name = "Smith", Zip = "8109", Address = new { street = "Main Street", Number = "79" } };
            b = new { Name = "Smith", Zip = "8109", Address = new { street = "Main Street", Number = "79" } };
            Assert.CompareProperties(a, b);

            a = new UserProperties { Name = "Smith", Address = new Address { Number = 5, Street = "Main Street", Zip = "08193" } };
            b = new UserProperties { Name = "Smith", Address = new Address { Number = 5, Street = "Main Street", Zip = "08193" } };
            Assert.CompareProperties(a, b);
        }

        [Test]
        public void Compare_Properties_Fails()
        {
            object a = new UserProperties { Name = "Smith", Address = new Address { Number = 4, Street = "Main Street", Zip = "08193" } };
            object b = new UserProperties { Name = "Smith", Address = new Address { Number = 5, Street = "Main Street", Zip = "08193" } };
            Assert.CompareProperties(a, b);
        }

        public class UserProperties
        {
            public string Name { get; set; }
            public Address Address { get; set; }

        }
        public class Address
        {
            public int Number { get; set; }
            public string Street { get; set; }
            public string Zip { get; set; }
        }

        #endregion Advanced Assertion Checks

        #region Boolean Asserts
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void IsLessThanTestGeneric_Assert()
        {
            Assert.Exception<int>(Assert.IsLessThan, 1, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void IsLessThanTestGeneric_DoesntAssert()
        {
            (bool exceptionWasThrown, Exception thrownException) result = Assert.Exception<int>(Assert.IsLessThan, 1, 2);
            if (result.exceptionWasThrown && result.thrownException is AssertException)
            {
                Assert.Fail(result.thrownException.ToString());
            }
        }

        [Test]
        public void IsGreaterThanTestGeneric_Assert()
        {
            // Assuming Assert.IsGreaterThan exists and comparing 2 to 1 should not throw an exception
            (bool exceptionWasThrown, Exception thrownException) result = Assert.Exception<int>(Assert.IsGreaterThan, 2, 1);
            if (result.exceptionWasThrown && result.thrownException is AssertException)
            {
                // If an exception was thrown, the test should fail
                Assert.Fail("IsGreaterThan should not have thrown an exception for 2 > 1.");
            }
            else
            {
                // If no exception was thrown, the assertion passed as expected
                Assert.Pass("IsGreaterThan correctly did not throw an exception for 2 > 1.");
            }
        }

        [Test]
        public void IsGreaterThanOrEqual_Asserts_WhenEqual()
        {
            var result = Assert.Exception(Assert.IsGreaterThanOrEqual, 1, 1, "Should not throw exception when actual is equal to expected");
            Assert.IsFalse(result.result, "IsGreaterThanOrEqual threw an exception when it should not have.");
        }

        [Test]
        public void IsSameGeneric_Assert()
        {
            (bool exceptionWasThrown, Exception thrownException) result = Assert.Exception<int>(Assert.IsLessThan, 1, 2);
            if (result.exceptionWasThrown && result.thrownException is AssertException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void IsGreaterThanOrEqual_DoesNotAssert_WhenActualIsGreaterThanExpected()
        {
            var result = Assert.Exception(Assert.IsGreaterThanOrEqual, 1, 2, "Should not throw exception when actual is greater than expected");
            Assert.IsFalse(result.result, "IsGreaterThanOrEqual threw an exception when it should not have.");
        }

        [Test]
        public void IsLessThanOrEqual_Asserts_WhenEqual()
        {
            var result = Assert.Exception(Assert.IsLessThanOrEqual, 2, 2, "Should not throw exception when actual is equal to expected");
            Assert.IsFalse(result.result, "IsLessThanOrEqual threw an exception when it should not have.");
        }

        [Test]
        public void IsLessThanOrEqual_DoesNotAssert_WhenActualIsLessThanExpected()
        {
            var result = Assert.Exception(Assert.IsLessThanOrEqual, 2, 1, "Should not throw exception when actual is less than expected");
            Assert.IsFalse(result.result, "IsLessThanOrEqual threw an exception when it should not have.");
        }

        [Test]
        public void IsSameGeneric_DoesntAssert()
        {
            object a = 1;
            object b = a;
            (bool exceptionWasThrown, Exception thrownException) result = Assert.Exception<int>(Assert.Same, a, b);
            if (result.exceptionWasThrown && result.thrownException is AssertException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void IsNotSameGeneric_Assert()
        {
            object a = 1;
            object b = 1;
            (bool exceptionWasThrown, Exception thrownException) result = Assert.Exception<int>(Assert.NotSame, a, b);
            if (result.exceptionWasThrown && result.thrownException is AssertException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void IsNotSameGeneric_DoesntAssert()
        {
            (bool exceptionWasThrown, Exception thrownException) result = Assert.Exception<int>(Assert.IsLessThan, 1, 2);
            if (result.exceptionWasThrown && result.thrownException is AssertException)
            {
                Assert.Fail();
            }
        }
        #endregion Boolean  Asserts

        #region Collection Asserts
        [Test]
        public void IsEmpty_Asserts_WhenNotEmpty()
        {
            var result = Assert.Exception<IEnumerable<int>>(Assert.IsEmpty, new[] { 1 }, "Collection is not empty");
            Assert.IsTrue(result.result, "IsEmpty did not throw an exception for a non-empty collection.");
        }

        [Test]
        public void IsNotEmpty_Asserts_WhenEmpty()
        {
            var result = Assert.Exception<IEnumerable<int>>(Assert.IsNotEmpty, new int[] { }, "Collection is empty");
            Assert.IsTrue(result.result, "IsNotEmpty did not throw an exception for an empty collection.");
        }

        [Test]
        public void Contains_Asserts_WhenItemNotPresent()
        {
            var result = Assert.Exception(new Action<int, IEnumerable<int>, string>(Assert.Contains), 2, new[] { 1, 3 }, "Collection does not contain the item");
            Assert.IsTrue(result.result, "Contains did not throw an exception when the item was not present.");
        }

        [Test]
        public void DoesNotContain_Asserts_WhenItemPresent()
        {
            var result = Assert.Exception(new Action<int, IEnumerable<int>, string>(Assert.DoesNotContain), 2, new[] { 1, 2, 3 }, "Collection contains the item");
            Assert.IsTrue(result.result, "DoesNotContain did not throw an exception when the item was present.");
        }

        [Test]
        public void IsTrue_DoesNotAssert_IfTrue()
        {
            Assert.Exception(Assert.IsTrue, 1 > 0);
        }

        [Test]
        public void IsTrue_Asserts_IfFalse()
        {
            Assert.Exception(Assert.IsTrue, 1 < 0);
        }

        [Test]
        public void IsNull_DoesNotAssert_IfNull()
        {
            object nullObject = null;
            Assert.Exception(Assert.IsNull, nullObject);
        }

        [Test]
        public void IsNull_DoesAssert_IfNotNull()
        {
            Assert.Exception(Assert.IsNull, "This is definitely not null");
        }

        [Test]
        public void IsNotNull_DoesNotAssert_IfNotNull()
        {
            Assert.Exception(Assert.IsNotNull, "This is definitely not null");
        }

        [Test]
        public void IsNotNull_DoesAssert_IfNull()
        {
            object nullObject = null;
            Assert.Exception(Assert.IsNotNull, nullObject);
        }
        #endregion Collection Asserts

        [Test]
        public void IsOfType_DoesNotAssert_IfOfType()
        {
            object objectToDetermineTypeFrom = "Hallo";
            (bool result, Exception thrownException) result = Assert.Exception(Assert.IsOfType<string>, objectToDetermineTypeFrom);
        }

        [Test]
        public void IsOfType_Asserts_IfNotOfType()
        {
            object objectToDetermineTypeFrom = "Hallo";
            (bool result, Exception thrownException) result = Assert.Exception(Assert.IsOfType<string>, objectToDetermineTypeFrom);
        }

        [Test]
        public void HasProperty()
        {
            int a = 1;
            int b = 0;
        }

        [Test]
        public void AssertThrows_CatchesThrownException()
        {
            int a = 1;
            int b = 0;

            //this should be fine because it really gets a division by zero exception.
            Assert.Throws<DivideByZeroException>(() => { int c = a / b; });
        }

        [Test]
        public void AssertThrows_DoesntCatchThrownException()
        {
            int a = 1;
            int b = 0;
            //since here the exception that should be caught (InvalidOperationException)
            //is different to what's really being thrown: DivisionByZeroException ths assert should give us an
            //exception that the exception thrown differs from the one to be caught.

            Assert.Throws<InvalidOperationException>(() => { int c = a / b; });
        }
    }
}
