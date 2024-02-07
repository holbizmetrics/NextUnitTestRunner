using NextUnit.Core.Asserts;
using NextUnit.Core.Extensions;
using NextUnit.Core.TestAttributes;
using System.Diagnostics;
using System.Reflection;

namespace AutoFixture.NextUnit.Tests
{
    public class AutoDataAttributeTests
    {
        [Test]
        [AutoData]
        [Group(nameof(AutoDataAttribute))]
        public void AutoDataNoParametersInjectingTest()
        {

        }

        /// <summary>
        /// If the AutoData is working correct all values should be containing something.
        /// </summary>
        /// <param name="intParameter"></param>
        /// <param name="boolParameter"></param>
        /// <param name="stringArray"></param>
        /// <param name="stringList"></param>
        [Test]
        [AutoData]
        [Group(nameof(AutoDataAttribute))]
        public void AutoDataIncludingSimpleParametersTests(int? intParameter, bool? boolParameter, string[] stringArray, List<string> stringList)
        {
            //Checks that intParameter is not null and has a valid value.
            Assert.IsNotNull(intParameter);
            Assert.IsTrue(intParameter.HasValue);

            //Checks that boolParameter is not null and has a valid value.
            Assert.IsNotNull(boolParameter);
            Assert.IsTrue(boolParameter.HasValue);

            Assert.IsNotNull(stringArray);
            Assert.IsNotEmpty(stringArray);

            Assert.IsNotNull(stringList);
            Assert.IsNotEmpty(stringList);
        }

        /// <summary>
        /// More sophisticated approach to check.
        /// Though, quite long here like this.
        /// </summary>
        [Test]
        [AutoData]
        [Group(nameof(AutoDataAttribute))]
        public void AutoDataAttribute()
        {
            MethodBase methodBase = new StackFrame().GetMethod();
            // In your test class or setup
            MethodInfo methodInfo = methodBase as MethodInfo;
            methodInfo.AssertMethodParameters(
                testInstance: this, // Pass an instance of the test class if the method is not static
                    assertAction: (parameterValue, parameterInfo) =>
                    {
                        Assert.IsNotNull((object)parameterValue);
                        Assert.HasProperty("HasValue", parameterInfo);
                        //Assert.IsTrue()
                    });
        }

        [Test]
        [AutoData]
        [Group(nameof(AutoDataAttribute))]
        public void AutoDataIncludingComplexParametersTests(Group group)
        {

        }
    }

    public class Group
    {
        public string Name { get; set; } = string.Empty;
        public int orderID { get; set; } = -1;
    }
}
