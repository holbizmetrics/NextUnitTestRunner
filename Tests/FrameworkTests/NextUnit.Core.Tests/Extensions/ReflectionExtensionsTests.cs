using Microsoft.CodeAnalysis.CSharp.Syntax;
using NextUnit.Core.Asserts;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using NextUnit.TestRunner.TestClasses;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NextUnit.Core.Tests.Extensions
{
    public class ReflectionExtensionsTests
    {
        [Test]
        public void IsComparableTest()
        {
             Assert.IsTrue(ReflectionExtensions.IsComparable(typeof(int)));
        }

        [Test]
        public void IsAsyncMethod_GivenAsyncMethod_ReturnsTrue_Test()
        {
            // Arrange
            MethodInfo methodInfo = this.GetType().GetMethod(nameof(AsyncMethodTest), BindingFlags.Public | BindingFlags.Instance);

            // Act
            bool result = ReflectionExtensions.IsAsyncMethod(methodInfo);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAsyncMethod_GivenNonAsyncMethod_ReturnsFalse()
        {
            // Arrange
            MethodInfo methodInfo = this.GetType().GetMethod(nameof(NonAsyncMethodTest));

            // Act
            bool result = ReflectionExtensions.IsAsyncMethod(methodInfo);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetMethodsWithAttributesAsIEnumerableGeneric2_FindsMethodsWithAttribute_Test()
        {
            // Arrange
            Type[] types = { typeof(ReflectionExtensionsTests) };

            // Act
            var result = ReflectionExtensions.GetMethodsWithAttributesAsIEnumerableGeneric2<TestAttribute>(types).ToList();

            // Assert
            //Assert.That(result, Has.Count.EqualTo(1));
            //Assert.That(result.First().Method.Name, Is.EqualTo(nameof(TestClassWithTestAttribute.TestMethod)));
        }

        [Test]
        public void HasAsyncMethodAttributes_GivenMethodWithoutAsyncAttributes_ReturnsFalse_Test()
        {
            // Arrange
            MethodInfo methodInfo = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithoutAttributesTest));

            // Act & Assert
            Assert.IsFalse(methodInfo.HasAsyncMethodAttributes());
        }

        [Test]
        public void HasAsyncMethodAttributes_GivenMethodWithAsyncAttributes_ReturnsTrue_Test()
        {
            // Arrange
            MethodInfo methodInfoMethodWithAsyncAttribute = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithAsyncAttribute));
            MethodInfo methodInfoMethodWithAsyncStateMachineAttributeAndAsyncKeyword = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithAsyncStateMachineAttributeAndAsyncKeyword));
            MethodInfo methodInfoMethodWithAsyncKeyword = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithAsyncKeyword));

            MethodInfo methodInfoMethodWithoutAttributesTest = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithoutAttributesTest));


            Assert.IsTrue(methodInfoMethodWithAsyncAttribute.HasAsyncMethodAttributes());
            Assert.IsTrue(methodInfoMethodWithAsyncStateMachineAttributeAndAsyncKeyword.HasAsyncMethodAttributes());
            Assert.IsTrue(methodInfoMethodWithAsyncKeyword.HasAsyncMethodAttributes());

            Assert.IsFalse(methodInfoMethodWithoutAttributesTest.HasAsyncMethodAttributes());
        }

        /// <summary>
        /// Without async keyword but the attribute is added to the test: should return true.
        /// </summary>
        [AsyncStateMachine(typeof(void))]
        public void MethodWithAsyncAttribute()
        {

        }

        //TODO: Check. Because this will NOT be executed like this, yet.
        // When executed should be returning true, because it has the attribute AND the async keyword.
        [AsyncStateMachineAttribute(typeof(void))]
        public async void MethodWithAsyncStateMachineAttributeAndAsyncKeyword()
        {

        }

        public async void MethodWithAsyncKeyword()
        {

        }


        public void MethodWithoutAttributesTest()
        {

        }

        [Test]
        [AsyncStateMachineAttribute(typeof(void))]
        public async Task AsyncMethodTest()
        {

        }

        [Test]
        public void NonAsyncMethodTest()
        {

        }
    }
}
