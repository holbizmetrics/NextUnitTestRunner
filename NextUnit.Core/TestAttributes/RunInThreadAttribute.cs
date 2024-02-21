using System.Diagnostics;
using System.Globalization;

namespace NextUnit.Core.TestAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RunInThreadAttribute : CommonTestAttribute
    {
        public ApartmentState ApartmentState { get; set; } = ApartmentState.STA;
        public CultureInfo CultureInfo { get; set; }
        public bool AddJoin { get; set; } = false;
        public bool IsBackground { get; set; } = false;

        public RunInThreadAttribute(ApartmentState apartmentState = ApartmentState.STA, bool addJoin = false, string cultureName = null)
        {
            ApartmentState = apartmentState;
            CultureInfo = CultureInfo.GetCultureInfo(cultureName);
            // Test logic here

            // Use 'output' to write to test output, if needed.
            Trace.WriteLine("Test Attribute that will cause running of the invoked method in a separate thread.");
        }
    }
}
