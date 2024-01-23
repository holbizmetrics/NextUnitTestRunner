using NextUnitHardwareContext.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnitHardwareContext.ExceptionHelpers
{
    public static class ExceptionHelpers
    {
        //TODO: this is not ready to use, yet. Because I want it to work as well with Exceptions that we didn't even got a reference to
        //using reflection.
        //public static string GetAllMessagesIncludingAdditionalProperties(this Exception exception)
        //{
        //    var messages = exception.FromHierarchy(ex => ex.InnerException)
        //        .Select(ex =>
        //        {
        //            var type = ex.GetType();
        //            var message = ex.Message;

        //            foreach (var property in type.GetProperties())
        //            {
        //                var value = property.GetValue(ex);
        //                message += $"{Environment.NewLine}{property.Name}: {value}";
        //            }

        //            return message;
        //        });

        //    return string.Join(Environment.NewLine, messages);
        //}
    }
}
