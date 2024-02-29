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
            MethodInfo methodInfo = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithAttributesTest));

            // Act
            bool result = ReflectionExtensions.HasAsyncMethodAttributes(methodInfo);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAsyncMethodAttributes_GivenMethodWithAsyncAttributes_ReturnsTrue_Test()
        {
            // Arrange
            MethodInfo methodInfo = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithAttributesTest));

            // Act
            bool result = ReflectionExtensions.HasAsyncMethodAttributes(methodInfo);

            // Assert
            Assert.IsTrue(result);
        }

        //[NullableContext(0)]
        [Test]
        public void MethodWithAttributesTest()
        {

        }

        //TODO: Check. Because this will NOT be executed like this, yet.
        [Test]
        [AsyncStateMachineAttribute(typeof(void))]
        public void MethodWithAsyncStateMachineAttributeAdditionallyTest()
        {

        }
        [Test]
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
