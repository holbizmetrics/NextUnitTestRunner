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
        public void IsAsyncMethod_GivenAsyncMethod_ReturnsTrue()
        {
            // Arrange
            MethodInfo methodInfo = this.GetType().GetMethod(nameof(AsyncMethod), BindingFlags.Public | BindingFlags.Instance);

            // Act
            bool result = ReflectionExtensions.IsAsyncMethod(methodInfo);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAsyncMethod_GivenNonAsyncMethod_ReturnsFalse()
        {
            // Arrange
            MethodInfo methodInfo = this.GetType().GetMethod(nameof(NonAsyncMethod));

            // Act
            bool result = ReflectionExtensions.IsAsyncMethod(methodInfo);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetMethodsWithAttributesAsIEnumerableGeneric2_FindsMethodsWithAttribute()
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
        public void HasAsyncMethodAttributes_GivenMethodWithoutAsyncAttributes_ReturnsFalse()
        {
            // Arrange
            MethodInfo methodInfo = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithoutAttributes));

            // Act
            bool result = ReflectionExtensions.HasAsyncMethodAttributes(methodInfo);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAsyncMethodAttributes_GivenMethodWithAsyncAttributes_ReturnsTrue()
        {
            // Arrange
            MethodInfo methodInfo = typeof(ReflectionExtensionsTests).GetMethod(nameof(ReflectionExtensionsTests.MethodWithAttributes));

            // Act
            bool result = ReflectionExtensions.HasAsyncMethodAttributes(methodInfo);

            // Assert
            Assert.IsTrue(result);
        }

        //[NullableContext(0)]
        [AsyncStateMachineAttribute(typeof(void))]
        [DebuggerStepThroughAttribute]
        public void MethodWithAttributes()
        {

        }

        public void MethodWithoutAttributes()
        {

        }

        public async Task AsyncMethod()
        {

        }

        public void NonAsyncMethod()
        {

        }
    }
}
