using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.Asserts
{
    /// <summary>
    /// Usage:
    /// bool result = Verifier.Verify(Assert.IsTrue, condition, "Condition should be true.");
    /// </summary>
    public static class Verifier
    {
        public static bool ThrowExceptions { get; set; } = true;

        public static bool Verify<T>(Func<T, bool> assertion, T value, string message = "")
        {
            var result = assertion(value);
            if (!result && ThrowExceptions)
            {
                throw new AssertException(message);
            }
            return result;
        }
    }
}
