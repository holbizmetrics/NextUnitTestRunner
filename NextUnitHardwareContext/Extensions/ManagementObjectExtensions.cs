using System.Management;
using System.Text;

namespace NextUnit.HardwareContext.Extensions
{
    public static class ManagementObjectExtensions
    {
        public static string ToFormattedString(this IEnumerable<ManagementObject> managementObjects)
        {
            var stringBuilder = new StringBuilder();
            foreach (var obj in managementObjects)
            {
                foreach (PropertyData property in obj.Properties)
                {
                    stringBuilder.AppendLine($"{property.Name}: {property.Value}");
                }
                stringBuilder.AppendLine(); // Separate each ManagementObject with a new line for readability
            }
            return stringBuilder.ToString();
        }
    }
}
