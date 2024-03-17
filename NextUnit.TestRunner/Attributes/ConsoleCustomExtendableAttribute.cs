using NextUnit.Core.TestAttributes;

namespace NextUnit.TestRunner.Attributes
{
    public class ConsoleCustomExtendableAttribute : CustomExtendableAttribute
    {
        public ConsoleCustomExtendableAttribute()
        {
            Console.WriteLine($"(Hello from {this.GetType()}: Hallo -------------------------------------------------------------------------------");
        }
    }
}
