//using System.Data.Entity.Validation;
using System.Text;

namespace NextUnit.Core
{
    /// <summary>
    /// Example of usage:
    ///     Exception exception = ExceptionFactory.CreateComplexNestedException();
    ///     string text = exception.GetAllMessagesIncludingAdditionalProperties();
    ///     
    /// Output example for ExceptionFactory             ExceptionFactory.CreateComplexNestedExampleException():
    /// 
    /// 
    /// </summary>
    public static class ExceptionManagerExtensions
    {
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return source.FromHierarchy(nextItem, s => s != null);
        }
        public static string GetAllMessages(this Exception exception)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException)
                .Select(ex => ex.Message);
            return string.Join(Environment.NewLine, messages);
        }

        public static string GetAllMessagesIncludingAdditionalProperties(this Exception exception)
        {
            var sb = new StringBuilder();
            GetAllMessagesRecursive(exception, sb);
            return sb.ToString();
        }

        private static void GetAllMessagesRecursive(Exception exception, StringBuilder sb, string indent = "")
        {
            if (exception == null) return;

            sb.AppendLine(indent + "Message: " + exception.Message);

            // Include all properties, but skip "Message" property since it was already added
            foreach (var property in exception.GetType().GetProperties())
            {
                if (property.Name != "Message")
                {
                    var value = property.GetValue(exception);
                    sb.AppendLine($"{indent}{property.Name}: {value}");
                }
            }

            sb.AppendLine(indent + "Data: " + exception.Data.Count + " additional exceptions");
            foreach (var key in exception.Data.Keys)
            {
                if (exception.Data[key] is Exception nestedException)
                {
                    sb.AppendLine(indent + "Nested exception for key " + key + ":");
                    GetAllMessagesRecursive(nestedException, sb, indent + "  ");
                }
            }
            if (exception.InnerException != null)
            {
                sb.AppendLine(indent + "InnerException:");
                GetAllMessagesRecursive(exception.InnerException, sb, indent + "  ");
            }
        }

        // Usage:
        //Exception topException = ExceptionFactory.CreateComplexNestedException();
        //string allMessages = topException.GetAllMessagesIncludingAdditionalProperties();
        //Trace.WriteLine(allMessages);
    }
}
