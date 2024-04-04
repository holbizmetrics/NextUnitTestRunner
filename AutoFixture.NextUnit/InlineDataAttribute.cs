using NextUnit.Core.TestAttributes;
using System.Globalization;
using System.Reflection;

namespace AutoFixture.NextUnit
{
    /// <summary>
    /// Defines inline data for a unit test marked with AutoFixture.AutoDataAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InlineDataAttribute : CommonTestAttribute
    {
        /// <summary>
        /// Gets or sets the custom display name for the test.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets data for calling test method.
        /// </summary>
        public object[] Data { get; }

        /// <summary>
        /// Initializes a new instance of the AutoFixture.InlineDataAttribute class.
        /// </summary>
        /// <param name="data1"></param>
        public InlineDataAttribute(object data1)
        {
            Data = new object[1] { data1 };
        }

        /// <summary>
        ///  Initializes a new instance of the AutoFixture.InlineDataAttribute class which
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="moreData"></param>
        public InlineDataAttribute(object data1, params object[] moreData)
        {
            if (moreData == null)
            {
                moreData = new object[1];
            }

            Data = new object[moreData.Length + 1];
            Data[0] = data1;
            Array.Copy(moreData, 0, Data, 1, moreData.Length);
        }

        /// <summary>
        /// Gets the data of the specifed medthodInfo
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            int num = Data.Length;
            //implement code here.
            return new object[1][] { Data };
        }

        /// <summary>
        /// Used to display the name.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (!string.IsNullOrWhiteSpace(DisplayName))
            {
                return DisplayName;
            }

            if (data != null)
            {
                return string.Format(CultureInfo.CurrentCulture, "{0} (Data Row {1})", methodInfo.Name, string.Join(",", data.AsEnumerable()));
            }

            return null;
        }
    }
}
