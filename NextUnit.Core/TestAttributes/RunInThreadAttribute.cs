using System.Diagnostics;

namespace NextUnit.Core.TestAttributes
{
    public class RunInThreadAttribute : CommonTestAttribute
    {
        protected Thread Thread { get; } = null;
        protected ApartmentState ApartmentState = ApartmentState.STA;

        public RunInThreadAttribute(ApartmentState apartmentState = ApartmentState.STA)
        {
            ApartmentState = apartmentState;
            var thread = new Thread(() =>
            {
                // Test logic here

                // Use 'output' to write to test output, if needed.
                Trace.WriteLine("Test running on separate thread.");
            });

            thread.SetApartmentState(ApartmentState.STA);
            Thread = thread;
            thread.Start();
            thread.Join();
        }
    }
}
