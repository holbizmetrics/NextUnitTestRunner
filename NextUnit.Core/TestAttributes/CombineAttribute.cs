namespace NextUnit.Core.TestAttributes
{
    namespace AutoFixture.NextUnit
    {
        /// <summary>
        /// If this attribute is set above a test only THEN the combinator is looking for combinations
        /// that may change the meaning.
        /// 
        /// Thus, if a test is defined like this:
        /// 
        /// [Test]
        /// [InjectData(1)]
        /// [Group("Hello")]
        /// public void Test()
        /// {
        /// }
        /// 
        /// a normal execution would happen.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class CombineAttribute : CommonTestAttribute
        {
            // This could hold specific configuration for the combination logic if needed.
        }
    }
}
