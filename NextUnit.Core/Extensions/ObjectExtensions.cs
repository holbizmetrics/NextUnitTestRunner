using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object value)
        {
            // Check if the value is already of the desired type
            if (value is T variable)
                return variable;

            // If the value is null, attempt to return the default value of T
            // This handles the case where T is a value type but the value is null
            if (value == null)
                return default;

            // Attempt to convert the value to the specified type using Convert.ChangeType,
            // which covers a wide range of direct conversions between compatible types
            try
            {
                // Special handling for nullable types
                Type targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                return (T)Convert.ChangeType(value, targetType);
            }
            catch (InvalidCastException)
            {
                // Return the default value of T if conversion failed
                return default;
            }
            catch (FormatException)
            {
                // Handle format exception if conversion is not possible due to incompatible formats
                return default;
            }
            catch (OverflowException)
            {
                // Handle cases where conversion failed due to overflow (e.g., converting a large number into a smaller type)
                return default;
            }
        }
    }
}
