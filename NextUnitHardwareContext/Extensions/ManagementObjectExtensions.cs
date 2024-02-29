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

        public static string ToFormattedString(this ManagementObject obj)
        {
            // Example: Extracting a few properties for demonstration purposes
            string manufacturer = obj["Manufacturer"]?.ToString() ?? "Unknown";
            string version = obj["Version"]?.ToString() ?? "Unknown";
            string serialNumber = obj["SerialNumber"]?.ToString() ?? "Unknown";

            // Format the string as needed
            return $"Manufacturer: {manufacturer}, Version: {version}, Serial Number: {serialNumber}";
        }
    }
}
