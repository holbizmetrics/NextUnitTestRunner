using NextUnit.Core.Asserts;

namespace NextUnit.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception[] ExtractExceptions(this Exception exception)
        {
            return ExtractExceptions<Exception>(exception);
        }

        public static T[] ExtractExceptions<T>(this Exception exception)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException);
            T[] exceptionsOfTypeT = messages.OfType<T>().ToArray();
            return exceptionsOfTypeT;
        }

        public static T[] ExtractAssertions<T>(this T exception) where T:Exception
        {
            var messages = exception.FromHierarchy(ex => (T)ex.InnerException);
            return (T[])messages;
        }

        public static string JoinExceptionTexts(Exception[] exceptions)
        {
            return string.Join(Environment.NewLine, exceptions.Select(ex => ex.Message));
        }

        public static string JoinExceptionTexts<T>(T[] exceptions) where T : Exception
        {
            return JoinExceptionTexts(exceptions.OfType<T>().ToArray());
        }

        public static string JoinExceptionTexts(this IEnumerable<Exception> exceptions)
        {
            if (exceptions == null)
            {
                return string.Empty;
            }

            return string.Join(Environment.NewLine, exceptions.Select(ex => ex.Message));
        }
    }
}
