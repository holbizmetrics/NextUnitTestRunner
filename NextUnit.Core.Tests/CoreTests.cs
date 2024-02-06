using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class CoreTests
    {
        [Test]
        [Group(nameof(Core))]
        public void AssertIsTrueTest_Successful_Test()
        {

        }
        
        [Test]
        [Group(nameof(Core))]
        public void AssertIsTrueTest_Fails_Test()
        {

        }

        [Test]
        [Group(nameof(Core))]
        public void AssertIsFalse_Successful_Test()
        { 
        }

        [Test]
        [Group(nameof(Core))]
        public void AssertIsFalse_Fails_Test()
        {
        }
    }
}
